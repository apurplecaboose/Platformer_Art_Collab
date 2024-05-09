using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfireRELOAD : MonoBehaviour
{
    P_ShootLogic P_shoot;
    [SerializeField] int _reloadAmount;
    public Particle_Master ParticlePrefab, ParticlePrefabUI;
    public float ParticleScale;
    bool _InReloadRange;
    public GameObject _CampfireReloadText;
    SlowMo SlowMoRef;
    Image _reloadUI;
    private void Awake()
    {
        P_shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ShootLogic>();
        SlowMoRef = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMo>();
        _reloadUI = GameObject.FindGameObjectWithTag("Reload_Particle_Point").GetComponent<Image>();
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
            Kachow.transform.localScale = new Vector3(ParticleScale, ParticleScale, 1);
            Kachow.Lifetime = 0.5f;


            Vector3 bulletCountUIPosition = Camera.main.ScreenToWorldPoint(_reloadUI.rectTransform.transform.position);
            Particle_Master KachowUI = Instantiate(ParticlePrefabUI, bulletCountUIPosition, Quaternion.identity);
            KachowUI.transform.localScale = new Vector3(ParticleScale, ParticleScale , 1);
            KachowUI.Lifetime = 0.5f;
            KachowUI.gameObject.GetComponent<MoveWithUI>().followTarget = _reloadUI;


            P_shoot.ReloadFireBullets(_reloadAmount);
            SlowMoRef.ReloadSlowMoTime();
            _InReloadRange = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) _InReloadRange = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) _InReloadRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) _InReloadRange = false;
    }
}
