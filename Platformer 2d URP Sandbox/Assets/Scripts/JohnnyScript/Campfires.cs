using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Campfires : MonoBehaviour
{

    GameObject _refToPlayer;
    PlayerControl _refToPlayerControl;
    SlowMo _refToSlowMo;
    Rigidbody2D _p_rb;
    [SerializeField] bool _isDash;
    [SerializeField] float _dashMultiplier;
    [SerializeField] Vector2 _MinandMaxCamfireForce;

    // Update is called once per frame
    private void Awake()
    {
        _dashMultiplier = 3f; //J;control dashing power 
        _refToPlayer = GameObject.FindGameObjectWithTag("Player");
        _p_rb = _refToPlayer.GetComponent<Rigidbody2D>();
        _refToPlayerControl = _refToPlayer.GetComponent<PlayerControl>();
        _refToSlowMo = _refToPlayer.GetComponent<SlowMo>();
    }
    private void Update()
    {
        CampFiresTp();//J: gather the function for campfire dashing in saperate script
                      //J;already save the campfire as profab,create variants prefab for different dashing power.
    }
    void CampFiresTp()
    {
        if (_isDash)
        {
            _refToSlowMo.SlowMoToggle = false;//J;return normal speed after shoot the campfire
            Vector2 dashDir;
            dashDir = transform.position - _refToPlayer.transform.position;
            dashDir.Normalize();
            float dashdistance = Vector3.Distance(transform.position, _refToPlayer.transform.position);
            dashdistance = Mathf.Clamp(dashdistance, _MinandMaxCamfireForce.x, _MinandMaxCamfireForce.y);
            if (Mathf.Sign(_p_rb.velocity.x) == Mathf.Sign(dashDir.x) && Mathf.Sign(_p_rb.velocity.y) == Mathf.Sign(dashDir.y))
            {
                //normal state
                _p_rb.AddForce(dashDir * dashdistance * _dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
                _isDash = false;
                return; // job well done solider return home!
            }
            if (Mathf.Sign(_p_rb.velocity.x) != Mathf.Sign(dashDir.x))
            {
                _p_rb.velocity = new Vector2(0, _p_rb.velocity.y); //cancel out x
            }
            if (Mathf.Sign(_p_rb.velocity.y) != Mathf.Sign(dashDir.y))
            {
                _p_rb.velocity = new Vector2(_p_rb.velocity.x, 0); //cancel out y
            }
            _p_rb.AddForce(dashDir * dashdistance * _dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
            _isDash = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            _isDash = true;
            Destroy(collision.gameObject);
        }
    }


}
