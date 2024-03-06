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
    [SerializeField] float _moveForce, _upThrust, _firePower, _blastPower;
    private Vector3 _refToMousePosition;
    public Vector2 BarrelBlastDir, blastDir;
    public Transform ShootDirection, ShootPoint;
    public Transform TeleportPos;
    public GameObject Bullet;
    public List<GameObject> NewBullet;
    public int BulletIndex=-1,BulletLimit;
    public bool IsBlast, IsDash, CanJump, Grounded,CanBeKnockBack, HaveAmmo;
    public LayerMask CheckGroundLayer;

    private void Awake()
    {
        P_rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
        Shoot(_firePower);
        BlastJump(_blastPower);
        TeleportLogic();
        Jump(_upThrust);
        EnterSlowMotion();

        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input

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

    public void BlastJump(float blastPower)
    {
        if (IsBlast)
        {
            P_rb.velocity = new Vector2(P_rb.velocity.x, 0); // cancel out gravity instantly
            blastDir = BarrelBlastDir.normalized;//normalize the Blast vector into direction only
            P_rb.AddForce(blastDir * blastPower, ForceMode2D.Impulse);
            IsBlast = false;
        }
    }

    void TeleportLogic()
    {

        if (IsDash)
        {
            Vector2 dashDir;
            dashDir = TeleportPos.transform.position - transform.position;
            float dashMultiplier = 3f; //E: I changed the value to be smaller after changing to impulse might need further tuning
            dashDir.Normalize();
            float dashdistance = Vector3.Distance(TeleportPos.transform.position, transform.position);
            dashdistance = Mathf.Clamp(dashdistance, 8, 10);
            P_rb.AddForce(dashDir * dashdistance * dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse

            IsDash = false;
        }
    }


    void Shoot(float firePower)
    {
        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction

        if (BulletLimit > 0)
        {
            HaveAmmo = true;
        }
        else { HaveAmmo = false; }//Setting bullet limits

        if (Input.GetKeyDown(KeyCode.Mouse0)&&HaveAmmo)
        {
            BulletLimit -= 1;//bullet limit set is 5
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            NewBullet.Add(BulletInstance);//record new bullet instantiate
            BulletIndex=NewBullet.Count-1;
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(_dir * firePower, ForceMode2D.Impulse);
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
