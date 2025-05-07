using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;
using static UnityEditor.MaterialProperty;
using System.Net.NetworkInformation;
using static NPCDialogue;

public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    private DialogueModel dialogueModel;
    private NPCBase currentNPC;
    private bool isWaitingForTouch = false; // 터치 입력 활성화 여부

    private int currentDialogueIndex = 0;

    private InputAction touchInput;  // 터치 입력

    private NPCDialogue currentDialogue;
    public NPCDialogue CurrentDialogue => currentDialogue;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueModel = new DialogueModel();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        // PC 마우스 입력 바인딩
        touchInput = new InputAction(binding: "<Mouse>/leftButton");
        touchInput.performed += ctx => OnScreenTouched();
        touchInput.Enable();
#else
                touchInput = new InputAction(binding: "<Touchscreen>/primaryTouch/tap");
                touchInput.performed += ctx => OnScreenTouched();
                touchInput.Enable();
#endif
    }

    //edit

    public class PlatformInputHandler : MonoBehaviour
    {
        private InputAction touchInput;



        void OnDisable()
        {
            touchInput?.Disable();

        }

        void OnScreenTouched()
        {
            Debug.Log("화면 터치 또는 클릭됨!");
            // 여기에 원하는 로직 추가
        }
    }


    private void Start()
    {
        StartCoroutine(LoadDialogueDate());
    }


    private IEnumerator LoadDialogueDate()
    {
        yield return StartCoroutine(dialogueModel.LoadCSV());
    }

    private void OnEnable()
    {
        touchInput.Enable();

    }

    private void OnDisable()
    {
        touchInput.Disable();

    }

    public void StartDialogue(NPCBase npc)
    {
        if (npc == null) return;

        currentNPC = npc;
        currentDialogueIndex = 0;

        //전체대화
        List<NPCDialogue> dialogues = dialogueModel.GetDialogues(npc.NPCID, npc._npcState);

        List<NPCDialogue> randomDialogues = dialogueModel.GetRandomDialogues(npc.NPCID);


        if (randomDialogues.Count > 0)
        {
            NPCDialogue selectedDialogue = randomDialogues[Random.Range(0, randomDialogues.Count)];

            int selectedIndex = dialogues.FindIndex(d => d.Dialogue_ID == selectedDialogue.Dialogue_ID);
            if (selectedIndex != -1)
            {
                currentDialogueIndex = selectedIndex;

            }
        }

        // if(currentDialogue != null) 
        currentDialogue = dialogues[currentDialogueIndex];


        if (dialogues.Count > 0)
        {
            DisplayCurrentDialogue();
        }
    }

    public void NextDialogue()
    {
        if (currentNPC == null) return;

        // 현재 NPC의 상태가 Simulation인지 확인하고 적절한 대화 리스트를 가져옴
        List<NPCDialogue> dialogues = dialogueModel.GetDialogues(currentNPC.NPCID,
                                currentNPC._npcState == ENPCState.Simulation ? ENPCState.Simulation : currentNPC._npcState);

        // 현재 진행 중인 시퀀스 확인 (리스트 범위 초과 방지)
        if (currentDialogueIndex < dialogues.Count - 1) // 다음 대화가 존재하는지 확인
        {
            if (dialogues[currentDialogueIndex].Sequence < dialogues[currentDialogueIndex + 1].Sequence)
            {
                currentDialogueIndex++;
                currentDialogue = dialogues[currentDialogueIndex];
                DisplayCurrentDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextDialogue_C(int choiceIndex)
    {
        if (currentNPC == null) return;


        // 현재 상태에서 적절한 대화 리스트 가져오기
        List<NPCDialogue> dialogues = dialogueModel.GetDialogues(currentNPC.NPCID,
        currentNPC._npcState == ENPCState.Simulation ? ENPCState.Simulation : currentNPC._npcState);


        if (currentDialogueIndex >= dialogues.Count) return;

        NPCDialogue currentDialogue = dialogues[currentDialogueIndex];

        if (currentDialogue.HasOptions && choiceIndex < currentDialogue.NextDialogueIDs.Count)
        {
            string nextDialogueID = currentDialogue.NextDialogueIDs[choiceIndex];
            NPCDialogue nextDialogue = dialogueModel.GetDialogueByID(nextDialogueID);
            currentDialogue = nextDialogue;

            if (nextDialogue != null)
            {
                // NPC 상태를 Simulation으로 변경
                //currentNPC.SetState(NPCState.Simulation);
                ENPCState state = (ENPCState)Enum.Parse(typeof(ENPCState), nextDialogue.State);
                currentNPC.SetState(state);

                // 선택한 nextDialogueID가 포함된 새로운 Simulation 대화 리스트를 가져옴
                List<NPCDialogue> simulationDialogues = dialogueModel.GetDialogues(currentNPC.NPCID, state);

                // nextDialogueID가 Simulation 리스트 내에서 몇 번째인지 찾음
                int newIndex = simulationDialogues.FindIndex(d => d.Dialogue_ID == nextDialogueID);

                if (newIndex != -1)
                {
                    // 현재 다이얼로그 인덱스를 새로운 흐름의 시작점으로 변경
                    currentDialogueIndex = newIndex;

                    DisplayCurrentDialogue();
                }
            }
        }
        else
        {
            NextDialogue();
        }
    }
    private void DisplayCurrentDialogue()
    {
        List<NPCDialogue> randomDialogues = dialogueModel.GetRandomDialogues(currentNPC.NPCID);


        List<NPCDialogue> dialogues = dialogueModel.GetDialogues(currentNPC.NPCID, currentNPC._npcState);
        NPCDialogue currentDialogue = dialogues[currentDialogueIndex];


        DialogueUI.Instance.ShowDialogue(currentDialogue);

        //선택지가 있는 경우 
        if (currentDialogue.HasOptions)
        {
            if (currentDialogue.PlayerChoices.Count > 0)    //Player가 선택해야할 대화가 있는 경우 
            {
                DialogueUI.Instance.ShowPlayerDialogueUI();
            }
 

            isWaitingForTouch = false; // 버튼이 있으면 터치 X
        }
        else
        {
            DialogueUI.Instance.HidePlayerDialogueUI();
            isWaitingForTouch = true;  // 버튼이 없으면 터치 O
        }
    }

    public void EndDialogue()
    {
        currentNPC.SetState(ENPCState.Default);
        DialogueUI.Instance.HideDialogue();

        isWaitingForTouch = false;
        currentNPC = null;
    }

    public void OnScreenTouched()
    {
        // 터치/클릭이 활성화된 경우에만 실행
        if (isWaitingForTouch && DialogueUI.Instance.IsDialogueActive())
        {
            NextDialogue();
        }
    }

    public void ChangeNPCState(ENPCState newState)
    {
        if (currentNPC == null) return;


        currentNPC.SetState(newState);
        currentDialogueIndex = 0;
        DisplayCurrentDialogue();

    }
    public void EnableTouch()
    {
        touchInput.Enable();
        isWaitingForTouch = true;

    }
    public void DisableTouch()
    {
        touchInput.Disable();
        isWaitingForTouch = false;
    }
}
