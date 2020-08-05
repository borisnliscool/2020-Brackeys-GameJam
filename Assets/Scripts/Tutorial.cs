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
    private bool tutorialOver = false;
    private bool textChanging;

    // Start is called before the first frame update
    void Start()
    {
        stringBuilder = new StringBuilder();
        enterText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeToNextMessage();
        }

        if(tutorialOver)
        {
            Debug.Log("Tutorial over");
        }
    }


    public void ChangeToNextMessage()
    {
        currentMessage++;
        if (textChanging)
        {
            Debug.LogError("Cannot clear text while changing.");
            return;
        }

        stringBuilder.Clear();
        tutorialText.text = "";
        enterText.gameObject.SetActive(false);

        if (currentMessage == messages.Count)
        {
            tutorialOver = true;
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
            enterText.gameObject.SetActive(true);
            yield break;
        }
        StartCoroutine(ChangeText());
    }
}
