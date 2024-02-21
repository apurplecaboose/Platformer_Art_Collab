using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D P_rb;
    float _xInput;
    [SerializeField]
    float _moveForce = 50f;

    public bool Grounded;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            P_rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
        PlayerInput();
    }
    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) _xInput = 0;
        else
        {
            if (Input.GetKey(KeyCode.Space)) // tapstrafe
            {
                if(Mathf.Sign(P_rb.velocity.x) != _xInput && _xInput != 0)
                {
                    float tapstrafeM = 1.15f;
                    P_rb.velocity = new Vector2(-P_rb.velocity.x * tapstrafeM, P_rb.velocity.y);
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        _xInput = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        _xInput = 1;
                    }
                    else _xInput = 0; //catch case
                }
            }
            else // not tapstrafe
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _xInput = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _xInput = 1;
                }
                else _xInput = 0; //catch case
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 xInputVec = new Vector2(_xInput, 0);
        P_rb.AddForce(xInputVec * _moveForce);
    }
}
