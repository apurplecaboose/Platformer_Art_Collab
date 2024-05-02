using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireRELOAD : MonoBehaviour
{
    P_ShootLogic P_shoot;
    [SerializeField] int _reloadAmount;
    public Particle_Master ParticlePrefab;
    public float ParticleScale;
    bool _triggerReload;
    private void Awake()
    {
        P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _triggerReload) 
        {
            Particle_Master Kachow = Instantiate(ParticlePrefab, this.transform.position, Quaternion.identity);
            Kachow.transform.localScale = new Vector3(ParticleScale, ParticleScale, 0);
            Kachow.Lifetime = 0.5f;
            P_shoot.ReloadFireBullets(_reloadAmount);
            _triggerReload = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _triggerReload = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _triggerReload = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _triggerReload = false;
    }
}
