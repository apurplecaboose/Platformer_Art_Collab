using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static PlayerState P_state;
    public static SceneEnum.SceneList SceneCache;
    public static bool Lv1Cleared, Lv2Cleared;
    public enum PlayerState
    {
        Playing,
        Win
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.Start();
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        P_state = PlayerState.Playing;
    }
}
