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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectBarrel(collision);
        DetectGround(collision);

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
            //E: changed to line below try not to use getcomponents durring runtime it will slow down the code especially when instance count increases
            //RefToPlayer.GetComponent<PlayerControl>().BarrelBlastDir = BlastDir; 
            _playerMoveRef.BarrelBlastDir = BlastDir;
            print("Detect" + BlastDir);
            //RefToPlayer.GetComponent<PlayerControl>().IsBlast = true;
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
}
