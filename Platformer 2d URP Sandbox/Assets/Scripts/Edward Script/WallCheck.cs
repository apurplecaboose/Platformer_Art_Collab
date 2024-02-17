using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public PlayerMove P_Ref;


    public LayerMask WallLayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((WallLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            P_Ref.Wallin = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if ((WallLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            P_Ref.Wallin = false;
        }
    }
}
