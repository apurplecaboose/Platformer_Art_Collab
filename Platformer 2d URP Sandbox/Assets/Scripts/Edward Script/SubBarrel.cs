using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBarrel : MonoBehaviour
{
    Barrel_Master _master;
    Vector2 _blastDir;
    void Start()
    {
        _master = this.transform.GetComponentInParent<Barrel_Master>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            _blastDir = _master.P_Ref.transform.position - this.transform.position;
            if (_master.PlayerInRange)//if player is in range players barrel can activate
            {
                KaBOOM();
                if(!_master.InvincibleBarrel)
                {
                    Particle_Master Kachow = Instantiate(_master.ParticlePrefab, this.transform.position, Quaternion.identity);
                    Kachow.transform.localScale = new Vector3(_master.ParticleScale, _master.ParticleScale, 0);
                    Kachow.Lifetime = 0.5f;

                    Destroy(transform.parent.gameObject, 0.025f); // destroy BarrelMaster after set time
                }
            }
            Destroy(collision.gameObject);
        }
    }
    void KaBOOM()
    {
        _master.P_rb.velocity = new Vector2(_master.P_rb.velocity.x, 0); // cancel out gravity instantly
        _blastDir = _blastDir.normalized;//normalize the Blast vector into direction only
        _master.P_rb.AddForce(_blastDir * _master.BarrelPower, ForceMode2D.Impulse);// use impulse for slow mo time
    }
}
