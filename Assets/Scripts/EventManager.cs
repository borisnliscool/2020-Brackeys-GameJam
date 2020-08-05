using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private PlayerController pController;
    [SerializeField] private Animator animControlDeath;
    private Finish finish;
    private bool inMainMenu;

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
    }

    void OnEnable()
    {
        if (inMainMenu)
        {
            return;
        }
        pController.PlayerDied += PController_PlayerDied;
        finish.LevelCompleted += Finish_LevelCompleted;
    }

    void OnDisable()
    {
        if (inMainMenu)
        {
            return;
        }
        pController.PlayerDied -= PController_PlayerDied;
        finish.LevelCompleted -= Finish_LevelCompleted;
    }

    private void Finish_LevelCompleted(string nextLevel)
    {
        LoadScene(nextLevel);
    }

    private void PController_PlayerDied()
    {
        animControlDeath.SetTrigger("PlayerDied");
        pController.PlayerDied -= PController_PlayerDied;
    }

    public void LoadScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }

}
