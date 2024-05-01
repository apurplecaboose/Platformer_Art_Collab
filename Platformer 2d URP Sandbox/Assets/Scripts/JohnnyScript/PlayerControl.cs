using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    SlowMo refToSlowMo;
    float _xInput;
    Rigidbody2D P_rb;
    [SerializeField] float _moveForce = 20f, _jumpForce = 7f, _tapStrafeMultiplier = 0.3f;
    [HideInInspector] public bool IsDash, CanJump, Grounded, CanBeKnockBack;
    public LayerMask CheckGroundLayer;
    [HideInInspector] public bool IsRight;
    public SpriteRenderer P_Anime_Sprite;
    P_Animation _P_anime;

    private void Awake()
    {
        refToSlowMo = this.GetComponent<SlowMo>();
        P_rb = this.GetComponent<Rigidbody2D>();
        _P_anime = P_Anime_Sprite.gameObject.GetComponent<P_Animation>();
        IsRight = true;
    }

    private void Update()
    {
        if (GameManager.P_state == GameManager.PlayerState.Playing)
        {
            PlayerInput();
            Jump(_jumpForce);
            EnterSlowMotion();
        }
        if (GameManager.P_state == GameManager.PlayerState.Win)
        {
            P_rb.velocity = new Vector2(0, P_rb.velocity.y); //remove player horizontal velocity but let player fall.
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.P_state == GameManager.PlayerState.Playing) // do not allow extra forces to be added when player has won.
        {
            Vector2 xInputVec = new Vector2(_xInput, 0);
            P_rb.AddForce(xInputVec * _moveForce);
        }
    }

    float _jumptimer, JumpCD = 0.1f;
    void Jump(float upThrust)
    {
        if (_jumptimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Grounded)
            {
                P_rb.AddForce(Vector2.up * upThrust, ForceMode2D.Impulse);
                Grounded = false;
                _jumptimer = JumpCD;
                //-------------------------------------------------------
                _P_anime.IsPlayJump = true;
                _P_anime.IsPlayRun = false;
                _P_anime.IsPlayIdle = false;
                //-------------------------------------------------------
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
                        P_Anime_Sprite.flipX = true;//J:Switch animation Sprite
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        _xInput = 1;

                        P_Anime_Sprite.flipX = false;//J:Switch animation Sprite
                    }
                    else
                    {
                        _xInput = 0; //catch case
                        _P_anime.IsPlayJump = true;
                        _P_anime.IsPlayIdle = false;
                        _P_anime.IsPlayRun = false;//--------May 1st modify the anime
                    }
                }
            }
            else // not tapstrafe
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _xInput = -1;
                    GetComponent<SpriteRenderer>().flipX = true;//J:Switch player Sprite
                    P_Anime_Sprite.flipX = true;//J:Switch animation Sprite

                    IsRight = false;//switch shooting point

                    //-------------------------------------------------------
                    _P_anime.IsPlayRun = true;
                    _P_anime.IsPlayJump = false;
                    _P_anime.IsPlayIdle = false;
                    //StartAnimation
                    //-------------------------------------------------------
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _xInput = 1;
                    GetComponent<SpriteRenderer>().flipX = false;//J:Switch player Sprite
                    P_Anime_Sprite.flipX = false;//J:Switch animation Sprite


                    IsRight = true;//switch shooting point
                    //-------------------------------------------------------
                    _P_anime.IsPlayRun = true;
                    _P_anime.IsPlayJump = false;
                    _P_anime.IsPlayIdle = false;
                    //StartAnimation
                    //-------------------------------------------------------
                }
                else
                {
                    _xInput = 0; //catch case
                    //-------------------------------------------------------
                    if (Grounded)
                    {
                        print("Work1");
                        _P_anime.IsPlayRun = false;
                        _P_anime.IsPlayJump = false;
                        _P_anime.IsPlayIdle = true;
                    }
                    //-------------------------------------------------------
                }
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
