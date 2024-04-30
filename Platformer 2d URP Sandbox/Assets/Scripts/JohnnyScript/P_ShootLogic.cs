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

    void Awake()
    {
        PlayerControlRef = this.GetComponent<PlayerControl>();
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveAmmo)
        {
            //------------------------
            _p_anime.IsPlayFire = true;
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
        if(Input.GetMouseButtonUp(0))
        {
            _p_anime.IsPlayFire = false;//turn off fire animation
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
