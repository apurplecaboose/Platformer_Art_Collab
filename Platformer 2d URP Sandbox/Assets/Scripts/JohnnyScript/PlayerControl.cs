using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    private float _xdir;
    private Rigidbody2D _refPlayerRb;
    [SerializeField] float _speed,_upThrust, _firePower,_blastPower;
    private Vector3 _refToMousePosition;
    public Transform ShootDirection,ShootPoint;
    public GameObject Bullet;
    public bool isBlast;
    private void Awake()
    {
        _refPlayerRb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input

        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(_dir);         
        }

        if (isBlast)
        {
            _refPlayerRb.AddForce(new Vector2(0, _blastPower), ForceMode2D.Impulse);
            isBlast = false;
        }



        Jump(_upThrust);
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


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    float blastPower = 15;
    //    if (collision.CompareTag("BlastBarrel"))
    //    {
    //        _refPlayerRb.AddForce(new Vector2(0, blastPower), ForceMode2D.Impulse);
    //    }
    //}

    void Shoot(Vector3 dir)
    {
        GameObject BulletInstance =Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
        //如果不用变量存储，脚本无法控制新生成游戏物体的组件对其进行编程（类似于Awake中绑定的步骤）
        BulletInstance.GetComponent<Rigidbody2D>().AddForce(dir*_firePower, ForceMode2D.Impulse);
    }
}
