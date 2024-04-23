using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletUI : MonoBehaviour
{
    GameObject _playerRef;
    P_ShootLogic _bulletref;

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        _bulletref = _playerRef.GetComponent<P_ShootLogic>();
    }
    private void Update()
    {
        Debug.Log(_bulletref.BulletNum);
    }
}
