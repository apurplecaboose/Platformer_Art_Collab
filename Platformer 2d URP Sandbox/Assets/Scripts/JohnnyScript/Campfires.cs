using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfires : MonoBehaviour
{

    GameObject _refToPlayer;
    PlayerControl _refToPlayerControl;
    Rigidbody2D _p_rb;
    [SerializeField] bool IsDash;
    [SerializeField] float dashMultiplier;

    // Update is called once per frame
    private void Awake()
    {
        dashMultiplier = 3f; //J;control dashing power 
        _refToPlayer = GameObject.FindGameObjectWithTag("Player");
        _p_rb = _refToPlayer.GetComponent<Rigidbody2D>();
        _refToPlayerControl= _refToPlayer.GetComponent<PlayerControl>();
    }
    private void Update()
    {
        CampFiresTp();//J: gather the function for campfire dashing in saperate script
                      //J;already save the campfire as profab,create variants prefab for different dashing power.
    }
    void CampFiresTp()
    {
        if (IsDash)
        {
            _refToPlayerControl.refToSlowMo.SlowMoToggle = false;//J;return normal speed after shoot the campfire
            Vector2 dashDir;
            dashDir = transform.position - _refToPlayer.transform.position;
            dashDir.Normalize();
            float dashdistance = Vector3.Distance(transform.position, _refToPlayer.transform.position);
            dashdistance = Mathf.Clamp(dashdistance, 8, 10);
            _p_rb.AddForce(dashDir * dashdistance * dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
            IsDash = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            IsDash = true;
        }
    }


}
