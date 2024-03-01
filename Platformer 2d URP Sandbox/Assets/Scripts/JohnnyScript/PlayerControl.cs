using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    public SlowMo refToSlowMo;
    private float _xdir;
    private Rigidbody2D _refPlayerRb;
    [SerializeField] float _speed, _upThrust, _firePower, _blastPower;
    private Vector3 _refToMousePosition;
    public Vector2 BarrelBlastDir, blastDir;
    public Transform ShootDirection, ShootPoint;
    public Transform TeleportPos;
    public GameObject Bullet;
    public List<GameObject> NewBullet;
    public int BulletIndex=-1;
    public bool IsBlast, IsDash, CanJump;
    public LayerMask CheckGroundLayer;

    private void Awake()
    {
        _refPlayerRb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Shoot(_firePower);
        BlastJump(_blastPower);
        TeleportLogic();
        GroundCheck();
        Jump(_upThrust);
        EnterSlowMotion();

        _xdir = Input.GetAxis("Horizontal");//player horizontal move input
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input

    }
    private void FixedUpdate()
    {
        HorizontalMove();
    }


    void HorizontalMove()
    {
        if (_xdir == 0)
        {
            _refPlayerRb.velocity = new Vector2(_refPlayerRb.velocity.x, _refPlayerRb.velocity.y);//avoid the override to rb velocity from x input when player experience the force of barrel
        }//blast blast state
        else
        {
            _refPlayerRb.velocity = new Vector2(_xdir * _speed, _refPlayerRb.velocity.y);
        }//normal state
    }

    void Jump(float upThrust)
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            _refPlayerRb.AddForce(new Vector2(0, upThrust), ForceMode2D.Impulse);
        }
    }

    public void BlastJump(float blastPower)
    {
        if (IsBlast)
        {
            blastDir = BarrelBlastDir.normalized;//normalize the Blast vector into direction only
            _refPlayerRb.AddForce(blastDir * blastPower);
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
            _refPlayerRb.AddForce(dashDir * dashdistance * dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
            IsDash = false;
        }
    }


    void Shoot(float firePower)
    {
        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            NewBullet.Add(BulletInstance);//record new bullet instantiate
            BulletIndex=NewBullet.Count-1;
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(_dir * firePower, ForceMode2D.Impulse);
        }
    }

    void GroundCheck()
    {
        float detectRange = 1f;
        RaycastHit2D _hitGround = Physics2D.Raycast(transform.position, -transform.up, detectRange, CheckGroundLayer);
        Debug.DrawRay(transform.position, -transform.up, Color.red, detectRange);
        if (_hitGround)
        {
            CanJump = true;
        }
        else { CanJump = false; }
    }

    void EnterSlowMotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            refToSlowMo.SlowMoToggle = !refToSlowMo.SlowMoToggle; // E: changed back to my slow mo
            //Time.timeScale = 0.3f;
        }
    }
}
