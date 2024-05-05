using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireRELOAD : MonoBehaviour
{
    P_ShootLogic P_shoot;
    [SerializeField] int _reloadAmount;
    public Particle_Master ParticlePrefab;
    public float ParticleScale;
    bool _InReloadRange;
    public GameObject _CampfireReloadText;
    SlowMo SlowMoRef;
    private void Awake()
    {
        P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        SlowMoRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMo>();
    }

    void Update()
    {
        if(_CampfireReloadText != null)
        {// if campfire text gameobject is linked then turn on and off text
            if(_InReloadRange)
            {
            _CampfireReloadText.SetActive(true);
            }
            else
            {
            _CampfireReloadText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _InReloadRange) 
        {
            Particle_Master Kachow = Instantiate(ParticlePrefab, this.transform.position, Quaternion.identity);
            Kachow.transform.localScale = new Vector3(ParticleScale, ParticleScale, 0);
            Kachow.Lifetime = 0.5f;
            P_shoot.ReloadFireBullets(_reloadAmount);
            SlowMoRef.ReloadSlowMoTime();
            _InReloadRange = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _InReloadRange = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _InReloadRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _InReloadRange = false;
    }
}
