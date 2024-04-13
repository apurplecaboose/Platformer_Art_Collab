using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    float WiningUnscaledCountDown;//Countdown for game wininig cinematics or whatever effects
    
    public enum GameStates
    {
        Menu,
        Playing,
        Death,
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
        if (gs == GameStates.Win)
        {
            WiningUnscaledCountDown -= Time.deltaTime;
        }
    }
}
