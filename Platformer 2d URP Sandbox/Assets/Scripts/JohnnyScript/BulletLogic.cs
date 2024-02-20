using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private GameObject RefToPlayer;
    private void Awake()
    {
        RefToPlayer = GameObject.Find("player").gameObject;
        print(RefToPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BlastBarrel"))
        {
            RefToPlayer.GetComponent<PlayerControl>().isBlast = true;
            Destroy(gameObject);
        }
    }
}
