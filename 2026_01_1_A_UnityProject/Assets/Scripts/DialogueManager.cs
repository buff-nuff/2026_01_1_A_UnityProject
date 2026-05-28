using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소 - 인스펙터 창에서 연결")]

    public GameObject DialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("기본 설정")]
    public Sprite defaultCharacterImage;

    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    private DialogueDataSO currentDialogue;
    private int currenLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCorouTine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(HandleNextInput);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextInput();
        }
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = "";

        for (int i =  0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void CompleteTyping()
    {
        if (typingCorouTine != null)
        {
            StopCoroutine(typingCorouTine);
        }

        isTyping = false;

        if (currentDialogue != null && currenLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currenLineIndex];
        }
    }

    void ShowCurrentLine()
    {
        if (currentDialogue != null && currenLineIndex < currentDialogue.dialogueLines.Count)
        {
            if (typingCorouTine != null)
            {
                StopCoroutine(typingCorouTine);
            }
        }

        string currentText = currentDialogue.dialogueLines[currenLineIndex];
        typingCorouTine = StartCoroutine(TypeText(currentText));
    }

    public void ShowNextLine()
    {
        currenLineIndex++;

        if (currenLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    void EndDialogue()
    {
        if (typingCorouTine != null)
        {
            StopCoroutine(typingCorouTine);
            typingCorouTine = null;
        }

        isDialogueActive = false;
        isTyping = false;
        DialoguePanel.SetActive(false);
        currenLineIndex = 0;
    }

    public void HandleNextInput()
    {
        if (isTyping && skipTypingOnClick)
        {
            CompleteTyping();
        }
        else if (!isTyping)
        {
            ShowNextLine();
        }
    }

    public void SkipDialogue()
    {
        EndDialogue();
    }

    public bool IsDialogueTctive()
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;

        currentDialogue = dialogue;
        currenLineIndex = 0;
        isDialogueActive = true;

        DialoguePanel.SetActive(true);
        characterNameText.text = dialogue.characterName;

        if (characterImage != null)
        {
            characterImage.sprite = dialogue.characterImage;
        }
        else
        {
            characterImage.sprite = defaultCharacterImage;
        }

        ShowCurrentLine();

    }
}
