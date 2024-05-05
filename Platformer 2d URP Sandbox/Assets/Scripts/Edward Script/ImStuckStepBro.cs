using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImStuckStepBro : MonoBehaviour
{
    GameObject stuckText;   
    P_ShootLogic _p_shoot;
    void Awake()
    {
        _p_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        stuckText = transform.GetChild(0).gameObject;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_p_shoot.CanShoot && _p_shoot.BulletNum <= 0) stuckText.SetActive(true);
        }
    }
}
