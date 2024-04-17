using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelParticle : MonoBehaviour
{

    private void OnEnable()
    {
        BarrelParticleEvent.EnableBarrelParticle += OnEnableBarrelParticle;
    }
    private void OnDisable()
    {
        BarrelParticleEvent.EnableBarrelParticle -= OnEnableBarrelParticle;
    }

    private void OnEnableBarrelParticle()
    {
        gameObject.SetActive(true);
    }
}
