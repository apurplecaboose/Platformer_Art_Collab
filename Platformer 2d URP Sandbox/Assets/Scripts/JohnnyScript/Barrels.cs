using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrels : MonoBehaviour
{

    GameObject _refToPlayer;
    PlayerControl _refToPlayerControl;
    Rigidbody2D _p_rb;
    [SerializeField] bool _isBlast;
    [SerializeField] float _blastPower;
    SlowMo _refToSlowMo;
    Vector2 BarrelBlastDir, blastDir;
    private void Awake()
    {
        _refToPlayer = GameObject.FindGameObjectWithTag("Player");
        _p_rb = _refToPlayer.GetComponent<Rigidbody2D>();
        _refToPlayerControl= _refToPlayer.GetComponent<PlayerControl>();
        _refToSlowMo = _refToPlayer.GetComponent<SlowMo>();
    }
    // Update is called once per frame
    void Update()
    {
        BlastJump(_blastPower);//J:create barrel with differet blast power by using prefab variants
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            BarrelBlastDir = _refToPlayer.transform.position - collision.collider.transform.position;
            if (_refToPlayerControl.CanBeKnockBack)//if player is in the knockback range,players then can be knocked back when bullet collide with barrels. 
            {
                _isBlast = true;
            }
            Destroy(collision.gameObject);
        }
    }

    public void BlastJump(float blastPower)
    {
        if (_isBlast)
        {
            _refToSlowMo.SlowMoToggle = false;//J;return normal speed after shoot the barrel
            _p_rb.velocity = new Vector2(_p_rb.velocity.x, 0); // cancel out gravity instantly
            blastDir = BarrelBlastDir.normalized;//normalize the Blast vector into direction only
            _p_rb.AddForce(blastDir * blastPower, ForceMode2D.Impulse);
            _isBlast = false;
            Destroy(gameObject);
        }
    }
}
