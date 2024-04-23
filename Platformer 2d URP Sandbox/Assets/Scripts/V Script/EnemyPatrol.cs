using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    GameObject enemy;
    Transform _pointA, _pointB;
    private float _time;
    [SerializeField] float _targetTime;

    private void Awake()
    {
        
        _pointA = transform.GetChild(0).GetComponent<Transform>();
        _pointB = transform.GetChild(1).GetComponent<Transform>();
        enemy = transform.GetChild(2).gameObject;
    }
    private void Update()
    {
        if(enemy != null)
        {
            _time += Time.deltaTime;
            enemy.transform.position = Vector2.Lerp(_pointA.position, _pointB.position, Mathf.PingPong(_time / _targetTime, 1f));
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
