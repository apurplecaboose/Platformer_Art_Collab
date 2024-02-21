using System.Collections;
using System.Collections.Generic;
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
        if (collision.collider.CompareTag("BlastBarrel"))
        {           
            BlastDir = RefToPlayer.transform.position - collision.collider.transform.position;
            RefToPlayer.GetComponent<PlayerControl>().BarrelBlastDir = BlastDir;
            print("Detect" + BlastDir);
            RefToPlayer.GetComponent<PlayerControl>().IsBlast = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CampFire"))
        {
            RefToPlayer.GetComponent<PlayerControl>().TeleportPos.Add(collision.gameObject.transform);
            RefToPlayer.GetComponent<PlayerControl>().CanTeleport = true;
            Destroy(gameObject);
        }
    }
}
