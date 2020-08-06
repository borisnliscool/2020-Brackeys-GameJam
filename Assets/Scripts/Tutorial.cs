using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public delegate void TutorialClose();
    public event TutorialClose TutorialClosed;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Text enterText;
    private StringBuilder stringBuilder;
    public List<string> messages = new List<string>();
    public int currentMessage;
    private bool textChanging, disableEnterKey;
    private ButtonScript tutorialButton;
    private PlayerMovement playerMovement;
    private PlayerController playerController;

    // Start is called before the first frame update

    void Awake()
    {
        stringBuilder = new StringBuilder();
        enterText.gameObject.SetActive(false);
        tutorialButton = FindObjectOfType<ButtonScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnEnable()
    {
        playerMovement.PlayerMoved += TutorialButton_TutorialButtonUsed;
        tutorialButton.TutorialButtonUsed += TutorialButton_TutorialButtonUsed;
        playerController.PlayerGhostMove += PlayerGhostMoved;

        playerMovement.tutorialFreeze = true;
        playerMovement.tutorialJumpFreeze = true;
        playerController.stopGhostSpawn = true;
    }

    

    void OnDisable()
    {
        playerController.PlayerGhostMove -= PlayerGhostMoved;
        playerMovement.PlayerMoved -= TutorialButton_TutorialButtonUsed;
        tutorialButton.TutorialButtonUsed -= TutorialButton_TutorialButtonUsed;
    }

    private void TutorialButton_TutorialButtonUsed()
    {
        ChangeToNextMessage();
    }

    private void PlayerGhostMoved()
    {
        ChangeToNextMessage();
        playerMovement.tutorialFreeze = false;
    }

    void Update()
    {
        if (disableEnterKey)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeToNextMessage();
        }

    }


    public void ChangeToNextMessage()
    {
        currentMessage++;

        

        if (currentMessage == 4)
        {
            playerController.stopGhostSpawn = false;
            playerMovement.tutorialFreeze = true;
        }

        stringBuilder.Clear();
        tutorialText.text = "";
        enterText.gameObject.SetActive(false);

        if (currentMessage == messages.Count)
        {
            TutorialClosed?.Invoke();
            return;
        }
        else
        {
            StartCoroutine(ChangeText());
        }
        
    }


    void PrintText(string text)
    {
        tutorialText.text = text;
    }

    private IEnumerator ChangeText()
    {

        if (currentMessage == 0 || currentMessage == 5)
        {
            disableEnterKey = false;
            enterText.gameObject.SetActive(true);
        }
        else
        {
            disableEnterKey = true;
            enterText.gameObject.SetActive(false);
        }

        if (!textChanging)
            textChanging = true;
        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < messages[currentMessage].Length; i++)
        {
            if (stringBuilder.Length == i)
            {
                stringBuilder.Append(messages[currentMessage][i]);
                break;
            }
            
        }
        
        PrintText(stringBuilder.ToString());
        if (stringBuilder.Length == messages[currentMessage].Length)
        {
            textChanging = false;

            if (currentMessage == 1)
            {
                playerMovement.tutorialFreeze = false;
            }

            if (currentMessage == 2)
            {
                playerMovement.tutorialJumpFreeze = false;
            }
            yield break;
        }
        StartCoroutine(ChangeText());
    }
}
