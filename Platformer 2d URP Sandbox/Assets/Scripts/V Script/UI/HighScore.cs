using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public float highscore, currentScore;
    public TextMeshProUGUI Timer, HighscoreTMP;
    public GameObject GM;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Update()
    {
        HighscoreTMP.text = highscore + "";
        currentScore = Timer.GetComponent<Timer>().timer;
        if (GM.GetComponent<GameMaster>().recording) //test condition
        {
            UpdateHighScore();
            
        }

    }
    public void UpdateHighScore()
    {
        if (highscore < currentScore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("HighScore", 0);
            Debug.Log("HighScore:" + highscore);
        }

    }

}