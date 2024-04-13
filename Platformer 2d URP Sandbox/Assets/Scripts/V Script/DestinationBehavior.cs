using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationBehavior : MonoBehaviour
{
    public GameObject GM;
    float UnscaledCountDown;//Countdown for game wininig cinematics or whatever effects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           
           LevelComplete();
        }
    }
    public void LevelComplete()
    {
        GM.GetComponent<GameMaster>().gs = GameMaster.GameStates.Win;


        /*ceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);*/
    }
}
