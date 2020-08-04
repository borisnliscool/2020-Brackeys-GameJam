using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private PlayerController pController;
    [SerializeField] private Animator animControlDeath;

    private void Awake()
    {
        pController = FindObjectOfType<PlayerController>();

    }

    void OnEnable()
    {
        pController.PlayerDied += PController_PlayerDied;
    }

    private void PController_PlayerDied()
    {
        animControlDeath.SetTrigger("PlayerDied");
    }

    public void LoadScene(string LevelName)
    {
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }

}
