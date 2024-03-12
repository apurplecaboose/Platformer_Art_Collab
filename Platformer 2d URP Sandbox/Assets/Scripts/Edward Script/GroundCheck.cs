using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMove P_Ref;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            P_Ref.Grounded = true;
        }
        if (collision.CompareTag("BlastBarrel"))
        {
            print(true);
            P_Ref.Grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            P_Ref.Grounded = false;
        }
        if (collision.CompareTag("BlastBarrel"))
        {
            P_Ref.Grounded = false;
            print(false);
        }
    }
}
