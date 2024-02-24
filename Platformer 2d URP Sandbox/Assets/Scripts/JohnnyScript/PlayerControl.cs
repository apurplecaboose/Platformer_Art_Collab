using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    private float _xdir;
    private Rigidbody2D _refPlayerRb;
    [SerializeField] float _speed,_upThrust, _firePower,_blastPower;
    private Vector3 _refToMousePosition;
    public Vector2 BarrelBlastDir, blastDir;
    public Transform ShootDirection,ShootPoint;
    public List<Transform> TeleportPos=new List<Transform>();
    public GameObject Bullet;
    public bool IsBlast,CanTeleport,CanJump;
    public LayerMask CheckGroundLayer;
    private void Awake()
    {
        _refPlayerRb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input
        blastDir = BarrelBlastDir.normalized;//normalize the Blast vector into direction only
        Shoot(_firePower);
        BlastJump(_blastPower);
        TeleportLogic();
        if (CanJump)
        {
            Jump(_upThrust);
        }

        GroundCheck();

    }
    private void FixedUpdate()
    {
        HorizontalMove(_speed);   
    }


    void HorizontalMove(float speed)
    {
        _xdir = Input.GetAxis("Horizontal");
        if (_xdir == 0)
        {
            _refPlayerRb.velocity = new Vector2(_refPlayerRb.velocity.x, _refPlayerRb.velocity.y);//avoid the override to rb velocity from x input
        }//blast state
        else
        {
            _refPlayerRb.velocity = new Vector2(_xdir * speed, _refPlayerRb.velocity.y);
        }//normal state

    }

    void Jump(float upThrust)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _refPlayerRb.AddForce(new Vector2(0, upThrust),ForceMode2D.Impulse);
        }       
    }

    public void BlastJump(float blastPower)
    {
        if (IsBlast)
        {
            _refPlayerRb.AddForce(blastDir * blastPower);
            IsBlast = false;
        }
    }

    void TeleportLogic()
    {
        if (CanTeleport)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale = 0.1f;
                transform.position = new Vector2(TeleportPos[0].transform.position.x, TeleportPos[0].transform.position.y+1);
                _refPlayerRb.velocity = Vector2.zero;
                CanTeleport = false;
                TeleportPos.Clear();
            }
        }
    }


    void Shoot(float firePower)
    {
        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1;
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            //如果不用变量存储，脚本无法控制新生成游戏物体的组件对其进行编程（类似于Awake中绑定的步骤）
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

}
  