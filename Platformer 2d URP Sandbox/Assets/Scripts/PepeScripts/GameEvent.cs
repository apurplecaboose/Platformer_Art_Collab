using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;
    private void Awake()
    {
        current = this;
    }
    public event Action onParticleTriggerEnter;
    public void ParticleTriggerEnter()
    {
        if(onParticleTriggerEnter != null)
        {
            onParticleTriggerEnter();
        }
    }
}
