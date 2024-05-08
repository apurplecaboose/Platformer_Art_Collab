using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Master : MonoBehaviour
{
    Vector3 _thisTransform;
    [HideInInspector] public float Lifetime;
    void Start()
    {
        if(Lifetime == 0) Lifetime = 5f; // default case if lifetime not set
        _thisTransform = this.transform.localScale;
        foreach (Transform child in transform)
        {
            child.localScale = _thisTransform;
        }
        Destroy(gameObject, Lifetime);
    }
}
