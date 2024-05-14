using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static PlayerState P_state;
    public static SceneEnum.SceneList SceneCache;
    public static bool Lv1Cleared, Lv2Cleared;

    public static bool CHEATS_ACTIVE;
    public static Nullable<Vector3> CheatPosition = null; // note to future edward ? makes a vec nullable EX: Vector3? to read use .value
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
    private void Update()
    {
        if(CHEATS_ACTIVE && P_state == PlayerState.Win) CheatPosition = null;
        if(SceneManager.GetActiveScene().name == SceneEnum.SceneList.Menu.ToString()) CheatPosition = null;
    }
}
