using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Projectile : MonoBehaviour
{
    //changed var names to fit naming convention
    GameObject _p_Ref;
    PlayerControl _playerMoveRef;
    Rigidbody2D _rb;
    SlowMo _slowMoRef;

    public Vector2 BlastDir;
    private void Awake()
    {
        //E: initalized all references on awake
        _p_Ref = GameObject.FindGameObjectWithTag("Player"); //E: changed to player tag therefore name of gameobject doesnt matter
        _playerMoveRef = _p_Ref.GetComponent<PlayerControl>(); //E: cache the reference
        _rb = this.GetComponent<Rigidbody2D>();
        _slowMoRef = _p_Ref.GetComponent<SlowMo>(); //temporary
    }

    private void Update()
    {
        RemoveBulletFromList();//Remove the unit of destroied bullets 
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



    void DetectBarrel(Collision2D cl)
    {
        if (cl.collider.CompareTag("BlastBarrel"))
        {
            BlastDir = _p_Ref.transform.position - cl.collider.transform.position;
            //E:  try not to use getcomponents durring runtime it will slow down the code especially when instance count increases
            _playerMoveRef.BarrelBlastDir = BlastDir;
            print("Detect" + BlastDir);
            _playerMoveRef.IsBlast = true;
            Destroy(gameObject);
        }
    }

    void DetectCampFire(Collider2D cl)
    {
        if (cl.CompareTag("CampFire"))
        {
            // changed back to using slow mo toggle
            //Time.timeScale = 1f;
            _slowMoRef.SlowMoToggle = false;
            //RefToPlayer.GetComponent<PlayerControl>().TeleportPos = cl.gameObject.transform;
            //RefToPlayer.GetComponent<PlayerControl>().IsDash = true;
            _playerMoveRef.TeleportPos = cl.gameObject.transform;
            _playerMoveRef.IsDash = true;
            Destroy(gameObject);
        }
    }
    void DetectGround(Collision2D cl)
    {
        if (cl.collider.CompareTag("Ground"))
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void DetectElseBullet(Collision2D cl)
    {
        if (cl.collider.CompareTag("Bullet"))
        {
            Destroy(_playerMoveRef.NewBullet[_playerMoveRef.BulletIndex]); //Destory bullet object         
        }
    }

    void RemoveBulletFromList()
    {
        for (int i = 0; i <=_playerMoveRef.BulletIndex; i++)
        {
            if (_playerMoveRef.NewBullet[i].gameObject == null)
            {
                _playerMoveRef.NewBullet.Remove(_playerMoveRef.NewBullet[i]);
                _playerMoveRef.BulletIndex-=1;
                print("Delete already");
            }
        }
    }
}
