using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float smoothSpeed;
    private Vector3 offset = new Vector3(0, 2, -10);

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPostion;
    }
}
