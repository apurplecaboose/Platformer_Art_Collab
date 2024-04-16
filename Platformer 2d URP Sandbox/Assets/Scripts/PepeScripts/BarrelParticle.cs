using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelParticle : MonoBehaviour
{
    public delegate void OnBarrelDestroy();
    public static event OnBarrelDestroy onBarrelDestroy;

    public void BarrelParticleActivate()
    {

    }
}
