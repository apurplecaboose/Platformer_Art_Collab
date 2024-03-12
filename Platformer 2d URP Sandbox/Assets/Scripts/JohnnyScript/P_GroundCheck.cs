using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundCheck : MonoBehaviour
{
    public PlayerControl P_Ref;

    public float CoyoteTime;
    [SerializeField]float _timer;
    private void Awake()
    {
        _timer = CoyoteTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("BlastBarrel"))//Change the tage"Floor" into "Ground"
        {
            P_Ref.Grounded = true;
            _timer = CoyoteTime;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("BlastBarrel"))//Change the tage"Floor" into "Ground"
        {
            P_Ref.Grounded = true;
            _timer = CoyoteTime;
        }
    }
    void Update()
    {
        if (P_Ref.Grounded)
        {
            _timer -= Time.deltaTime;
        }

        if( _timer <= 0 && P_Ref.Grounded)
        {
            P_Ref.Grounded = false;
        }
    }
}
