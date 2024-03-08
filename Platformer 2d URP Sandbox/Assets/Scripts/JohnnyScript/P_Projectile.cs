using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Projectile : MonoBehaviour
{
    //changed var names to fit naming convention
    GameObject _p_Ref;
    PlayerControl _playerMoveRef;
    P_ShootLogic _p_shootLogic;
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
        _p_shootLogic=_p_Ref.GetComponent<P_ShootLogic>();
    }

    private void Update()
    {
        RemoveBulletFromList();//Remove the unit of destroied bullets 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectGround(collision);
        DetectElseBullet(collision);
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
            Destroy(_p_shootLogic.NewBullet[_p_shootLogic.BulletIndex]); //Destory bullet object         
        }
    }

    void RemoveBulletFromList()
    {
        for (int i = 0; i <= _p_shootLogic.BulletIndex; i++)
        {
            if (_p_shootLogic.NewBullet[i].gameObject == null)
            {
                _p_shootLogic.NewBullet.Remove(_p_shootLogic.NewBullet[i]);
                _p_shootLogic.BulletIndex-=1;
                print("Delete already");
            }
        }
    }
}
