using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Rigidbody2D rB;
    // Start is called before the first frame update
    void Awake()
    {
        rB = GetComponent<Rigidbody2D>();
    }
}
