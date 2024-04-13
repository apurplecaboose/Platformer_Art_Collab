using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    public float highscore,currentScore;
    public TextMeshProUGUI Timer;
    public GameObject GM;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Update()
    {
        if (GM.GetComponent<GameMaster>().gs == GameMaster.GameStates.Win)
        {
            UpdateHighScore();
        }
    }
    public void UpdateHighScore()
    {
        if (highscore > currentScore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("HighScore",0);
        }

    }

}
