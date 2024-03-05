using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundCheck : MonoBehaviour
{
    public PlayerControl P_Ref;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))//Change the tage"Floor" into "Ground"
        {
            P_Ref.Grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))//Change the tage"Floor" into "Ground"
        {
            P_Ref.Grounded = false;
        }
    }
}
