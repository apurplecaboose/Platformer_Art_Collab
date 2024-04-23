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
    public bool HaveAmmo;
    PlayerControl PlayerControlRef;
    public LanternLight _p_LanternLight;

    void Awake()
    {
        PlayerControlRef = this.GetComponent<PlayerControl>();
    }

    void Update()
    {
        Shoot();
        ReloadFireBullets();//Test Only
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input
    }

    void Shoot()
    {
        //        _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        //        ShootDirection.up = _dir;// shooting direction
        //ShootDirection.up = _dir;// shooting direction

        if (BulletNum > 0)
        {
            HaveAmmo = true;
        }
        else { HaveAmmo = false; }//Setting bullet limits

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveAmmo)
        {
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
