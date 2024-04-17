using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BarrelParticleEvent
{
    public static event Action EnableBarrelParticle;
    public static void CallEnableBarrelParticle()
    {
        EnableBarrelParticle?.Invoke();
    }
}

