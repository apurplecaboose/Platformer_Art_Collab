using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDistanceLimit : MonoBehaviour
{

    Vector3 startPos;
    

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        SelfDestructRange(5f);
    }



    
    void SelfDestructRange(float Range)
    {

        if (Vector2.Distance(transform.position, startPos) > Range)
        {
            Destroy(gameObject);
        }
    }
    void SelfDestructTime(float timeTilDestruct)
    {
        timeTilDestruct -= Time.deltaTime;
        if(timeTilDestruct <= 0)
        {
            Destroy(gameObject);
        }
    }


}