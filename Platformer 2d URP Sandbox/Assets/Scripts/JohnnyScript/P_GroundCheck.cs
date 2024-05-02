using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundCheck : MonoBehaviour
{
    public PlayerControl P_Ref;

    public float CoyoteTime;
    [SerializeField]float _timer;
    bool _framespikePrevention = false;
    int _deltaTimeBuffer = 1;
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
            _framespikePrevention = true;
            _deltaTimeBuffer = 1;
            if(Time.timeScale <1)
            {
                _timer = CoyoteTime + 0.2f;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("BlastBarrel"))//Change the tage"Floor" into "Ground"
        {
            P_Ref.Grounded = true;
            _timer = CoyoteTime;
            _framespikePrevention = true;
            _deltaTimeBuffer = 1;
            if (Time.timeScale < 1)
            {
                _timer = CoyoteTime + 0.2f;
            }
        }
    }
    private void FixedUpdate()
    {
        _deltaTimeBuffer -= 1;
    }
    void Update()
    {
        if(_deltaTimeBuffer <= 0)
        {
            _framespikePrevention = false;
        }

    }
    private void LateUpdate()
    {
        if (P_Ref.Grounded && !_framespikePrevention)
        {
            _timer -= Time.unscaledDeltaTime;
        }

        if (_timer <= 0 && P_Ref.Grounded)
        {
            P_Ref.Grounded = false;
        }
    }
}
