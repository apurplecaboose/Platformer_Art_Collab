using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_Projectile_Pepe : MonoBehaviour
{
    //changed var names to fit naming convention
    [SerializeField] float _firePower;
    GameObject _p_Ref;
    PlayerControl _playerMoveRef;
    P_ShootLogic_Pepe _p_shootLogic;
    Rigidbody2D _rb;
    SlowMo _slowMoRef;

    public Vector2 BlastDir;

    float _bulletTimer;
    bool _isGrounded = false;
    private void Awake()
    {
        //E: initalized all references on awake
        _p_Ref = GameObject.FindGameObjectWithTag("Player"); //E: changed to player tag therefore name of gameobject doesnt matter
        _playerMoveRef = _p_Ref.GetComponent<PlayerControl>(); //E: cache the reference
        _rb = this.GetComponent<Rigidbody2D>();
        _slowMoRef = _p_Ref.GetComponent<SlowMo>(); //temporary
        _p_shootLogic = _p_Ref.GetComponent<P_ShootLogic_Pepe>();

        _bulletTimer = 1;//how long will bullets exist
    }

    private void Update()
    {
        DestroyBullet();//Destroy bullets

        if (!_isGrounded)
        {
            BulletShooting(_firePower);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectBarrel(collision);
        DetectGround(collision);
        DetectElseBullet(collision);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectCampFire(collision);
    }

    void BulletShooting(float firePower)
    {
        _rb.AddForce(_p_shootLogic._dir.normalized * firePower, ForceMode2D.Impulse);
    }

    void DetectBarrel(Collision2D collision)
    {
        if (collision.collider.CompareTag("BlastBarrel"))
        {
            BlastDir = _p_Ref.transform.position - collision.collider.transform.position;
            //E:  try not to use getcomponents durring runtime it will slow down the code especially when instance count increases
            _playerMoveRef.BarrelBlastDir = BlastDir;
            if (_playerMoveRef.CanBeKnockBack)//if player is in the knockback range,players then can be knocked back when bullet collide with barrels. 
            {
                _playerMoveRef.IsBlast = true;
            }
            //_playerMoveRef.IsBlast = true;
            Destroy(gameObject);
        }
    }

    void DetectCampFire(Collider2D collision)
    {
        if (collision.CompareTag("CampFire"))
        {
            _slowMoRef.SlowMoToggle = false;
            _playerMoveRef.TeleportPos = collision.gameObject.transform;
            _playerMoveRef.IsDash = true;
            Destroy(gameObject);
        }
    }
    void DetectGround(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
            _rb.bodyType = RigidbodyType2D.Static;
            _bulletTimer += 6;//bullets will exist extra long when they collide with grounds or walls
        }
    }

    void DetectElseBullet(Collision2D cl)
    {
        //if (cl.collider.CompareTag("Bullet"))
        //{
        //    Destroy(_p_shootLogic.NewBullet[_p_shootLogic.BulletIndex]); //Destory bullet object         
        //}
    }

    void DestroyBullet()
    {
        _bulletTimer -= Time.deltaTime;
        if (_bulletTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
