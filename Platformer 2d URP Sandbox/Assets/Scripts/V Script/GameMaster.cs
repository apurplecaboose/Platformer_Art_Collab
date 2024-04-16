using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    float WiningUnscaledCountDown;//Countdown for game wininig cinematics or whatever effects
    public bool recording;
    public enum GameStates
    {
        Menu,
        Playing,
        Death,
        Record,
        Win
    }
    public GameStates gs;
    // Start is called before the first frame update
    void Start()
    {
        WiningUnscaledCountDown = 3;
        gs = GameStates.Playing;//not sure if there should be a menu state
    }

    // Update is called once per frame
    void Update()
    {
        if (gs == GameStates.Record)
        {
            recording = true;
            gs = GameStates.Win;
           
        }
        else
        {
            recording = false;
        }

    }
}
