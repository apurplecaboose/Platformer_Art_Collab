using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPurposeMovement : MonoBehaviour
{
    void Movement()
    {
        float speed = 0.1f;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed,0 );
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3( speed,0);
        }
    }
    private void Update()
    {
        Movement();
    }

}
