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
        
        highscore = PlayerPrefs.GetFloat("HighScore", 101);
    }
    private void Update()
    {
        //if (highscore > 100)
        //{
        //    HighscoreTMP.text = "Highscore:N/A";
        //}
        if (highscore < 100)
        {
            HighscoreTMP.text = highscore + "";

        }
        else
        {
            HighscoreTMP.text = "Highscore:N/A";
        }
        currentScore = Timer.GetComponent<Timer>().timer;
        if (GM.GetComponent<GameMaster>().recording) //test condition
        {
            UpdateHighScore();
        }

    }
    public void UpdateHighScore()
    {
        if (highscore > currentScore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highscore);
            Debug.Log("HighScore:" + highscore);
        }

    }

}
