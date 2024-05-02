using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootLogic : MonoBehaviour
{
    Vector3 _refToMousePosition;
    public Transform RightShootPoint,LeftShootPoint;
    public int BulletNum = 10;
    public GameObject BulletPrefab;
    [HideInInspector]public bool HaveAmmo;
    PlayerControl PlayerControlRef;
    public LanternLight _p_LanternLight;
    public P_Animation _p_anime;
    public bool IsAwake, CanShoot;
    public float CountDown_fire_anime,Shoot_Interval;

    void Awake()
    {
        PlayerControlRef = this.GetComponent<PlayerControl>();
        Shoot_Interval = 0.1f;
        CanShoot =true;
    }

    void Update()
    {
        if(GameManager.P_state == GameManager.PlayerState.Playing)
        {
            Shoot();
            ReloadFireBullets();//Test Only
            _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input
        }
    }

    void Shoot()
    {

        if (BulletNum > 0)
        {
            HaveAmmo = true;
        }
        else { HaveAmmo = false; }//Setting bullet limits

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveAmmo&&CanShoot)
        {
            //------------------------
            _p_anime.IsPlayFire = true;
            CanShoot = false;
            CountDown_fire_anime = 0.1f;
            //------------------------
            BulletNum -= 1;//bullet limit set is 10
            if(PlayerControlRef.IsRight)//switch shooting point
            {
                GameObject BulletInstance = Instantiate(BulletPrefab, RightShootPoint.position, transform.rotation);//J:Change the shooting position    
                P_Projectile projectileScript = BulletInstance.GetComponent<P_Projectile>(); // cache script ref
                Vector3 shootdir = new Vector3(_refToMousePosition.x - RightShootPoint.position.x/*transform.position.x*/, _refToMousePosition.y - RightShootPoint.position.y/*transform.position.y*/);
                projectileScript.ShootDir = shootdir;
                projectileScript.PlayerIntialPosition = this.transform.position;
                _p_LanternLight.TriggerLightChange(BulletNum);
            }
            if (PlayerControlRef.IsRight==false)//switch shooting point
            {
                GameObject BulletInstance = Instantiate(BulletPrefab, LeftShootPoint.position, transform.rotation);//J:Change the shooting position    
                P_Projectile projectileScript = BulletInstance.GetComponent<P_Projectile>(); // cache script ref
                Vector3 shootdir = new Vector3(_refToMousePosition.x - LeftShootPoint.position.x/*transform.position.x*/, _refToMousePosition.y - LeftShootPoint.position.y/*transform.position.y*/);
                projectileScript.ShootDir = shootdir;
                projectileScript.PlayerIntialPosition = this.transform.position;
                _p_LanternLight.TriggerLightChange(BulletNum);
            }
        }
        if (CanShoot == false)
        {
            Shoot_Interval -= Time.deltaTime;
            if (Shoot_Interval <= 0)
            {              
                CanShoot = true;
                Shoot_Interval = 0.1f;
                CountDown_fire_anime = 0.15f;
                _p_anime.IsPlayIdle = false;

            }
        }
        if (CountDown_fire_anime>0)
        {
            CountDown_fire_anime-=Time.smoothDeltaTime;
            if (CountDown_fire_anime <= 0)
            {
                _p_anime.IsPlayFire = false;
                CountDown_fire_anime = 0.1f;
                
            }
        }
      
    }

    void ReloadFireBullets()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletNum = 10;
            _p_LanternLight.TriggerLightChange(BulletNum);
        }
    }
}
