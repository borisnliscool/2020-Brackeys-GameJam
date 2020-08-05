using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private PlayerController pController;
    [SerializeField] private Animator animControlDeath, animControlGhostDeath, animControlTutorial;
    private Finish finish;
    private bool inMainMenu;
    private GhostController gController;
    private Tutorial tutorial;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            inMainMenu = true;
        }

        if (inMainMenu)
        {
            return;
        }
        pController = FindObjectOfType<PlayerController>();
        finish = FindObjectOfType<Finish>();
        gController = FindObjectOfType<GhostController>();
        tutorial = FindObjectOfType<Tutorial>();
    }

    void OnEnable()
    {
        if (inMainMenu)
        {
            return;
        }
        gController.PlayerTurnedToGhost += GController_PlayerTurnedToGhost;
        pController.PlayerDied += PController_PlayerDied;
        finish.LevelCompleted += Finish_LevelCompleted;
        tutorial.TutorialClosed += Tutorial_TutorialClosed;

        animControlGhostDeath.gameObject.SetActive(false);
        animControlDeath.gameObject.SetActive(false);
    } 

    void OnDestroy()
    {
        if (inMainMenu)
        {
            return;
        }
        tutorial.TutorialClosed -= Tutorial_TutorialClosed;
        gController.PlayerTurnedToGhost -= GController_PlayerTurnedToGhost;
        pController.PlayerDied -= PController_PlayerDied;
        finish.LevelCompleted -= Finish_LevelCompleted;
    }

    public void UnloadGhostUI()
    {
        FreezeTime.i.TimeUnfreezeRequest();
        animControlGhostDeath.gameObject.SetActive(false);
        animControlGhostDeath.SetTrigger("GhostAlive");
    }

    private void Tutorial_TutorialClosed()
    {
        // 
        animControlTutorial.SetTrigger("TutorialClosed");
    }

    private void Finish_LevelCompleted(string nextLevel)
    {
        LoadScene(nextLevel);
    }

    private void PController_PlayerDied()
    {
        animControlDeath.gameObject.SetActive(true);
        animControlDeath.SetTrigger("PlayerDied");
        pController.PlayerDied -= PController_PlayerDied;
    }

    private void GController_PlayerTurnedToGhost()
    {
        FreezeTime.i.TimeFreezeRequest();
        animControlGhostDeath.gameObject.SetActive(true);
        animControlGhostDeath.SetTrigger("GhostDied");
    }

    public void LoadScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }

}
