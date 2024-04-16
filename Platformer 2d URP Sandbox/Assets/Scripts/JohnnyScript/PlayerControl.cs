using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public SlowMo refToSlowMo;
    float _xInput;
    private Rigidbody2D P_rb;
    [SerializeField] float _moveForce, _jumpForce, _tapStrafeMultiplier;
    public bool IsDash, CanJump, Grounded, CanBeKnockBack;
    public LayerMask CheckGroundLayer;
    public PlayerState RefPlayerState;
    public SpriteRenderer LanternStick, LanternStick1, Lantern,Lantern1;
    public enum PlayerState
    {
        InGame,
        Win,
        Lose
    }

    private void Awake()
    {
        P_rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (RefPlayerState == PlayerState.InGame)
        {
            PlayerInput();
            Jump(_jumpForce);
            EnterSlowMotion();
        }
        if(RefPlayerState == PlayerState.Win)
        {
            //P_rb.bodyType = RigidbodyType2D.Static;
            P_rb.velocity = new Vector2(0, P_rb.velocity.y); //remove player horizontal velocity but let player fall.
        }
    }
    private void FixedUpdate()
    {
        //if (RefPlayerState == PlayerState.InGame)
        //{
        //}
        if (RefPlayerState == PlayerState.InGame) // do not allow extra forces to be added when player has won.
        {
            Vector2 xInputVec = new Vector2(_xInput, 0);
            P_rb.AddForce(xInputVec * _moveForce);
        }
    }

    float _jumptimer, JumpCD = 0.1f;
    void Jump(float upThrust)
    {
        if(_jumptimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Grounded)
            {
                P_rb.AddForce(Vector2.up * upThrust, ForceMode2D.Impulse);
                Grounded = false;
                _jumptimer = JumpCD;
            }
        }
        else _jumptimer -= Time.deltaTime;
    }

    void EnterSlowMotion()
    {
        //changed slowmo to hold not toggle
        if (Input.GetKey(KeyCode.Mouse1))
        {
            refToSlowMo.SlowMoToggle = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            refToSlowMo.SlowMoToggle = false;
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
                   // expressed as percent momentum transfer 1 being full momentum transfer
                    P_rb.velocity = new Vector2(-P_rb.velocity.x * _tapStrafeMultiplier, P_rb.velocity.y);
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        _xInput = -1;
                        transform.rotation = Quaternion.Euler(0, 180, 0);//J:rotate player when change direction
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        _xInput = 1;
                        transform.rotation = Quaternion.Euler(0, 0, 0);//J:rotate player when change direction
                    }
                    else _xInput = 0; //catch case
                }
            }
            else // not tapstrafe
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _xInput = -1;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _xInput = 1;
                    transform.rotation = Quaternion.Euler(0, 0, 0);//rotate player when change direction

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
