using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    public delegate void FinishLevel(string nextLevel);
    public event FinishLevel LevelCompleted;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            LevelCompleted?.Invoke(nextLevel);
        }
    }
}
