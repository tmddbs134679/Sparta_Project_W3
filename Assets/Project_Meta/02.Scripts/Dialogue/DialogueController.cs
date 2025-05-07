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
    private bool isWaitingForTouch = false; // ��ġ �Է� Ȱ��ȭ ����

    private int currentDialogueIndex = 0;

    private InputAction touchInput;  // ��ġ �Է�

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
        // PC ���콺 �Է� ���ε�
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
            Debug.Log("ȭ�� ��ġ �Ǵ� Ŭ����!");
            // ���⿡ ���ϴ� ���� �߰�
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

        //��ü��ȭ
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

        // ���� NPC�� ���°� Simulation���� Ȯ���ϰ� ������ ��ȭ ����Ʈ�� ������
        List<NPCDialogue> dialogues = dialogueModel.GetDialogues(currentNPC.NPCID,
                                currentNPC._npcState == ENPCState.Simulation ? ENPCState.Simulation : currentNPC._npcState);

        // ���� ���� ���� ������ Ȯ�� (����Ʈ ���� �ʰ� ����)
        if (currentDialogueIndex < dialogues.Count - 1) // ���� ��ȭ�� �����ϴ��� Ȯ��
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


        // ���� ���¿��� ������ ��ȭ ����Ʈ ��������
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
                // NPC ���¸� Simulation���� ����
                //currentNPC.SetState(NPCState.Simulation);
                ENPCState state = (ENPCState)Enum.Parse(typeof(ENPCState), nextDialogue.State);
                currentNPC.SetState(state);

                // ������ nextDialogueID�� ���Ե� ���ο� Simulation ��ȭ ����Ʈ�� ������
                List<NPCDialogue> simulationDialogues = dialogueModel.GetDialogues(currentNPC.NPCID, state);

                // nextDialogueID�� Simulation ����Ʈ ������ �� ��°���� ã��
                int newIndex = simulationDialogues.FindIndex(d => d.Dialogue_ID == nextDialogueID);

                if (newIndex != -1)
                {
                    // ���� ���̾�α� �ε����� ���ο� �帧�� ���������� ����
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

        //�������� �ִ� ��� 
        if (currentDialogue.HasOptions)
        {
            if (currentDialogue.PlayerChoices.Count > 0)    //Player�� �����ؾ��� ��ȭ�� �ִ� ��� 
            {
                DialogueUI.Instance.ShowPlayerDialogueUI();
            }
 

            isWaitingForTouch = false; // ��ư�� ������ ��ġ X
        }
        else
        {
            DialogueUI.Instance.HidePlayerDialogueUI();
            isWaitingForTouch = true;  // ��ư�� ������ ��ġ O
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
        // ��ġ/Ŭ���� Ȱ��ȭ�� ��쿡�� ����
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
