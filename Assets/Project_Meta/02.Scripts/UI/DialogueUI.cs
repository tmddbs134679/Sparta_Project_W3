using Meta;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image npcImage;
    [SerializeField] private TextMeshProUGUI npcNameText;

    [SerializeField] private Button Dialogue1Button;
    [SerializeField] private Button Dialogue2Button;
    [SerializeField] private TextMeshProUGUI playerDialogue1_text;
    [SerializeField] private TextMeshProUGUI playerDialogue2_text;



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

        AddListener();

    }

    void AddListener()
    {
   
        if (Dialogue1Button != null && Dialogue2Button != null)
        {

            Dialogue1Button.onClick.AddListener(() => DialogueController.Instance.NextDialogue_C(0));
            Dialogue2Button.onClick.AddListener(() => DialogueController.Instance.NextDialogue_C(1));
        }
    }

    public void UpdateDialogueText()
    {
        if (DialogueController.Instance.CurrentDialogue != null)
        {
            ShowDialogue(DialogueController.Instance.CurrentDialogue);
        }
    }
    public void ShowDialogue(NPCDialogue dialogue)
    {
    
        dialogueText.text = dialogue.Text;
        npcNameText.text = dialogue.NPC_Name;
        LoadNPCImage(dialogue.NPC_ID);

        if (dialogue.PlayerChoices.Count > 1)
        {
            playerDialogue1_text.text = dialogue.PlayerChoices[0];
            playerDialogue2_text.text = dialogue.PlayerChoices[1];
        }
     
        UIPanel.SetActive(true);
    }
    public void LoadNPCImage(string imageName)
    {
        Sprite sprite = Resources.Load<Sprite>($"NPC/{imageName}");
        if (sprite == null)
        {
            Debug.LogError($"이미지 {imageName} 가 존재하지 않습니다.");
        }

        npcImage.sprite = sprite;
       
    }
    public void HideDialogue()
    {
        UIPanel.SetActive(false);
    }

    public bool IsDialogueActive()
    {
        return gameObject.activeSelf;
    }
    public void ShowPlayerDialogueUI()
    {
        Dialogue1Button.gameObject.SetActive(true);
        Dialogue2Button.gameObject.SetActive(true);
    }

    public void HidePlayerDialogueUI()
    {
        if (Dialogue1Button != null && Dialogue2Button != null)
        {
            Dialogue1Button.gameObject.SetActive(false);
            Dialogue2Button.gameObject.SetActive(false);
        }
    }


}
