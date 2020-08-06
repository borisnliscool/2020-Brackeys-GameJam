using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float time;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            time = 0;
        }
        else
        {
            time = PlayerPrefs.GetFloat("time");
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        timerText.text = time.ToString();
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat("time", time);
    }
}
