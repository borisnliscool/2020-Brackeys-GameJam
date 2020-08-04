using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerController player;
    public float smoothSpeed;
    private Vector3 offset = new Vector3(0, 2, -10);
    private bool trackingPlayer = true;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        player.PlayerDied += Player_PlayerDied;
    }
    private void OnDestroy()
    {
        if (player == null)
        {
            return;
        }
        player.PlayerDied -= Player_PlayerDied;
    }

    private void Player_PlayerDied()
    {
        trackingPlayer = false;
        player.PlayerDied -= Player_PlayerDied;
    }

    private void LateUpdate()
    {
        if (!trackingPlayer)
        {
            return;
        }
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;
    }
}
