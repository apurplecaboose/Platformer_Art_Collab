using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnum : MonoBehaviour
{
    public enum SceneList
    {
        scene_1,
        scene_2,
        scene_3,
        Menu,
        PreCutScene1,
        PreCutScene2,
        PreCutScene3,
        YouDied,
        EndCutScene,
    }

    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}
