using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public SlowMo refToSlowMo;
    private float _xInput;
    private Rigidbody2D P_rb;
    [SerializeField] float _moveForce, _upThrust;
    public bool IsDash, CanJump, Grounded, CanBeKnockBack;
    public LayerMask CheckGroundLayer;

    private void Awake()
    {
        P_rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
        Jump(_upThrust);
        EnterSlowMotion();

    }
    private void FixedUpdate()
    {
        Vector2 xInputVec = new Vector2(_xInput, 0);
        P_rb.AddForce(xInputVec * _moveForce);
    }


    void Jump(float upThrust)
    {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            P_rb.AddForce(Vector2.up * upThrust, ForceMode2D.Impulse);
        }
    }

    void EnterSlowMotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            refToSlowMo.SlowMoToggle = !refToSlowMo.SlowMoToggle; // E: changed back to my slow mo
            //Time.timeScale = 0.3f;
        }
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) _xInput = 0;
        else
        {
            if (/*Input.GetKey(KeyCode.Space)*/!Grounded) // tapstrafe
            {
                if (Mathf.Sign(P_rb.velocity.x) != _xInput && _xInput != 0)
                {
                    float tapstrafeM = 0.5f; // expressed as percent momentum transfer 1 being full momentum transfer
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlastRange"))
        {
            CanBeKnockBack = true;//when enter the blast range player can be knocked back.

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlastRange"))
        {
            CanBeKnockBack = false;//when leave the blast range player will not be knocked back.
        }
    }
}
