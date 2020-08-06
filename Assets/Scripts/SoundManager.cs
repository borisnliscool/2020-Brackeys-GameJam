using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    PlayerMovement playerM;
    GhostController ghostC;
    AudioSource audioPlayer;
    [SerializeField] private AudioClip playerMadeGhostSFX;
    [SerializeField] private AudioClip playerJumpedSFX;
    // Start is called before the first frame update
    void Awake()
    {
        playerM = FindObjectOfType<PlayerMovement>();
        audioPlayer = GetComponent<AudioSource>();
        ghostC = FindObjectOfType<GhostController>();
    }


    void OnEnable()
    {
        playerM.PlayerJumped += PlayerM_PlayerJumped;
        ghostC.PlayerCreatedGhost += GhostC_PlayerCreatedGhost;
    }
   

    void OnDisable()
    {
        playerM.PlayerJumped -= PlayerM_PlayerJumped;
        ghostC.PlayerCreatedGhost -= GhostC_PlayerCreatedGhost;
    }

    private void GhostC_PlayerCreatedGhost()
    {
        audioPlayer.clip = playerMadeGhostSFX;
        audioPlayer.Play();
    }
    private void PlayerM_PlayerJumped()
    {
        audioPlayer.clip = playerJumpedSFX;
        audioPlayer.Play();
    }
}
