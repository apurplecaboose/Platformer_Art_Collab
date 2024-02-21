using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    private float _xdir;
    private Rigidbody2D _refPlayerRb;
    [SerializeField] float _speed,_upThrust, _firePower,_blastPower;
    private Vector3 _refToMousePosition, _blastDirection,_blastDir;
    public Transform ShootDirection,ShootPoint;
    public List<Transform> TeleportPos=new List<Transform>();
    public GameObject Bullet;
    public bool IsBlast,CanTeleport;
    public GameObject Barrel;

    private void Awake()
    {
        _refPlayerRb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input

        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction

        Vector3 tempPos = this.transform.position- Barrel.transform.position;
        //_blastDirection = new Vector3(Barrel.transform.position.x - transform.position.x, Barrel.transform.position.y - transform.position.y,0);//
        _blastDir = tempPos.normalized;

        Shoot(_dir, _firePower);
        BlastJump(_blastPower);
        TeleportLogic();
        //Jump(_upThrust);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _refPlayerRb.AddForce(new Vector2(100,0));
        }


    }
    private void FixedUpdate()
    {
        HorizontalMove(_speed);
       
    }


    void HorizontalMove(float speed)
    {
        _xdir = Input.GetAxis("Horizontal");
        _refPlayerRb.velocity = new Vector2(_xdir * speed,_refPlayerRb.velocity.y);
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
            Vector2 blast = new Vector2(_blastDir.x, _blastDir.y);
            print(blast);
            _refPlayerRb.AddForce(blast * blastPower);
            IsBlast = false;
        }
    }

    void TeleportLogic()
    {
        if (CanTeleport)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                transform.position = TeleportPos[0].transform.position;
                CanTeleport = false;
            }
        }
    }


    void Shoot(Vector3 dir,float firePower)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            //如果不用变量存储，脚本无法控制新生成游戏物体的组件对其进行编程（类似于Awake中绑定的步骤）
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(dir* firePower, ForceMode2D.Impulse);
        }      
    }
}
