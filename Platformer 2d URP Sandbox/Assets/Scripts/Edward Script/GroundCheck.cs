using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMove P_Ref;

    public LayerMask GroundLayer;
    public LayerMask WallLayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((GroundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            P_Ref.Grounded = true;
        }
        if (!P_Ref.Grounded)
        {
            if ((WallLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                P_Ref.Grounded = true;
            }
        }
    }
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if ((GroundLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
    //    {
    //        P_Ref.Grounded = false;
    //    }
    //    if (P_Ref.Grounded)
    //    {
    //        if ((WallLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
    //        {
    //            P_Ref.Grounded = false;
    //        }
    //    }
    //}
}
