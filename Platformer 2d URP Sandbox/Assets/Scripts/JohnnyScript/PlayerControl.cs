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
    SpriteRenderer _P_Anime_SR;
    public Animator _p_Anime;
    P_ShootLogic _P_Shoot;
    float _jump_Intervel, _jumpTimer_anime;
    public bool _startJump,_startFalling;

    public ParticleSystem Dust;
    private void Awake()
    {
        refToSlowMo = this.GetComponent<SlowMo>();
        P_rb = this.GetComponent<Rigidbody2D>();
        _P_Shoot = this.GetComponent<P_ShootLogic>();
        _P_Anime_SR = _p_Anime.gameObject.GetComponent<SpriteRenderer>();
        IsRight = true;

        _jump_Intervel = 0.1f;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Update()
    {
        if (GameManager.P_state == GameManager.PlayerState.Playing)
        {
            PlayerInput();
            Jump(_jumpForce);
            EnterSlowMotion();
            DetectFalling();

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
                _startJump = true;
                P_rb.AddForce(Vector2.up * upThrust, ForceMode2D.Impulse);
                Grounded = false;
                _jumptimer = JumpCD;
                Dust.Play();
            }
        }
        else _jumptimer -= Time.deltaTime;
    }

    void DetectFalling()
    {
        if (!Grounded)
        {
            _startFalling=true;
            if (_startFalling && !_startJump)
            {
                _p_Anime.SetBool("IsFalling", true);
                _p_Anime.SetBool("IsJumping", false);
            }
        }
        if (Grounded)
        {
            _startFalling = false;
            _p_Anime.SetBool("IsFalling", false);
        }
        if (_startJump)
        {
            _p_Anime.SetBool("IsJumping", true);
            _p_Anime.SetBool("IsFalling", false);
            _jumpTimer_anime += Time.deltaTime;
            if (_jumpTimer_anime >= _jump_Intervel)
            {
                _startJump = false;
                _startFalling = true;
                _jumpTimer_anime = 0f;
            }
        }

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
                        if (!_P_Shoot.IsShootFlip)
                        {
                            _P_Anime_SR.flipX = true;//J:Switch animation Sprite
                        }

                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        _xInput = 1;
                        if (!_P_Shoot.IsShootFlip)
                        {
                            _P_Anime_SR.flipX = false;//J:Switch animation Sprite
                        }
                    }
                    else
                    {
                        // E: Play falling animation
                        _xInput = 0; //catch case
                        _p_Anime.SetBool("IsJumping", true);//--------May 1st modify the anime
                        _p_Anime.SetBool("IsIdle", false);
                        _p_Anime.SetBool("IsRunning", false);
                    }
                }
            }
            else // not tapstrafe
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _xInput = -1;
                    if (!_P_Shoot.IsShootFlip)
                    {
                        _P_Anime_SR.flipX = true;//J:Switch animation Sprite
                        _p_Anime.SetBool("IsRunning", true);
                    }
                    else
                    {
                        _p_Anime.SetBool("IsRunning", false);
                    }

                    IsRight = false;//switch shooting point
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _xInput = 1;
                    if (!_P_Shoot.IsShootFlip)
                    {
                        _P_Anime_SR.flipX = false;//J:Switch animation Sprite
                        _p_Anime.SetBool("IsRunning", true);
                    }
                    else
                    {
                        _p_Anime.SetBool("IsRunning", false);
                    }
                    IsRight = true;//switch shooting point

                    _p_Anime.SetBool("IsJumping", false);
                }
                else
                {
                    _xInput = 0; //catch case
                                 //-------------------------------------------------------
                    _p_Anime.SetBool("IsIdle", true);
                    _p_Anime.SetBool("IsRunning", false);
                    _p_Anime.SetBool("IsJumping", false);
                    _p_Anime.SetBool("IsFalling", false);
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
