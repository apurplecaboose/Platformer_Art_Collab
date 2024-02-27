using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private GameObject RefToPlayer;
    public Vector2 BlastDir;
    private void Awake()
    {
        RefToPlayer = GameObject.Find("player").gameObject;
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
            BlastDir = RefToPlayer.transform.position - cl.collider.transform.position;
            RefToPlayer.GetComponent<PlayerControl>().BarrelBlastDir = BlastDir;
            print("Detect" + BlastDir);
            RefToPlayer.GetComponent<PlayerControl>().IsBlast = true;
            Destroy(gameObject);
        }
    }

    void DetectCampFire(Collider2D cl)
    {
        if (cl.CompareTag("CampFire"))
        {
            RefToPlayer.GetComponent<PlayerControl>().TeleportPos = cl.gameObject.transform;
            RefToPlayer.GetComponent<PlayerControl>().IsDash = true;
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
    void DetectGround(Collision2D cl)
    {
        if (cl.collider.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
