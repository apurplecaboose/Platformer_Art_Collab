using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootLogic : MonoBehaviour
{
    private Vector3 _refToMousePosition;
    public Transform ShootDirection, ShootPoint;
    public int BulletNum;
    public GameObject BulletPrefab;
    public bool HaveAmmo;

    public LanternLight _p_LanternLight;

    //public Vector3 _dir;
    // Update is called once per frame
    void Update()
    {
        Shoot();
        ReLoad();//Test Only
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
            GameObject BulletInstance = Instantiate(BulletPrefab, transform.position, transform.rotation);
            
            P_Projectile projectileScript = BulletInstance.GetComponent<P_Projectile>(); // cache script ref
            Vector3 shootdir = new Vector3(_refToMousePosition.x - transform.position.x, _refToMousePosition.y - transform.position.y);
            projectileScript.ShootDir = shootdir;
            projectileScript.PlayerIntialPosition = this.transform.position;
            _p_LanternLight.TriggerLightChange(BulletNum);
        }
    }

    void ReLoad()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletNum = 10;
        }

    }
}
