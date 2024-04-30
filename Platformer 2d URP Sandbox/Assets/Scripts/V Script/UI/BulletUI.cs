using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletUI : MonoBehaviour
{
    GameObject _playerRef;
    P_ShootLogic _bulletref;
    [SerializeField] TextMeshProUGUI _bulletTMPref;

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        _bulletref = _playerRef.GetComponent<P_ShootLogic>();
    }
    private void Update()
    {
        _bulletTMPref.text = _bulletref.BulletNum+"";
    }
}
