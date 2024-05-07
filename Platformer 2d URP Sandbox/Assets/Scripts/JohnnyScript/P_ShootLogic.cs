using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootLogic : MonoBehaviour
{
    Vector3 _refToMousePosition;
    public Transform RightShootPoint, LeftShootPoint;
    public int BulletNum = 10;
    public GameObject BulletPrefab;
    [HideInInspector] public bool HaveAmmo;
    PlayerControl PlayerControlRef;
    public LanternLight _p_LanternLight;
    public bool IsAwake, IsShootFlip, CanShoot;
    float _Shoot_Interval, _Flip_Interval,_Flip_Timer;
    public Animator P_Anime;
    public AnimationClip ShootAnimationClip;
    SpriteRenderer _P_Anime_SR;

    void Awake()
    {
        PlayerControlRef = this.GetComponent<PlayerControl>();
        _Shoot_Interval = 0.1f;
        _Flip_Interval = ShootAnimationClip.length;
        IsShootFlip = false;
        CanShoot = true;
        _P_Anime_SR = P_Anime.gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        ReloadFireBullets(BulletNum);
    }

    void Update()
    {
        if (GameManager.P_state == GameManager.PlayerState.Playing)
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveAmmo&& CanShoot)
        {
            IsShootFlip=true;
            if (_refToMousePosition.x > transform.position.x)
            {
                _P_Anime_SR.flipX = false;
                
            }
            if (_refToMousePosition.x < transform.position.x)
            {
                _P_Anime_SR.flipX = true;
            }
            //------------------------
            P_Anime.SetBool("IsFiring",true);
            P_Anime.Play("Shoot", 0, 0);

            CanShoot = false;
            //------------------------
            BulletNum -= 1;//bullet limit set is 10
            if (PlayerControlRef.IsRight)//switch shooting point
            {
                GameObject BulletInstance = Instantiate(BulletPrefab, RightShootPoint.position, transform.rotation);//J:Change the shooting position    
                P_Projectile projectileScript = BulletInstance.GetComponent<P_Projectile>(); // cache script ref
                Vector3 shootdir = new Vector3(_refToMousePosition.x - RightShootPoint.position.x/*transform.position.x*/, _refToMousePosition.y - RightShootPoint.position.y/*transform.position.y*/);
                projectileScript.ShootDir = shootdir;
                projectileScript.PlayerIntialPosition = this.transform.position;
                _p_LanternLight.TriggerLightChange(BulletNum);
            }
            if (PlayerControlRef.IsRight == false)//switch shooting point
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
            _Shoot_Interval -= Time.deltaTime;
            if (_Shoot_Interval <= 0)
            {
                CanShoot = true;
                _Shoot_Interval = 0.1f;
                PlayerControlRef._p_Anime.SetBool("IsIdle", false);

            }
        }

        if (IsShootFlip == true)
        {
            _Flip_Timer += Time.deltaTime;
            if (_Flip_Timer >= _Flip_Interval)
            {
                IsShootFlip = false;
                P_Anime.SetBool("IsFiring", false);
                _Flip_Timer = 0;
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
    /// <summary>
    /// pass in the reload amount and it will do the rest for you
    /// </summary>
    /// <param name="bulletNumber"></param>
    public void ReloadFireBullets(int bulletNumber)
    {
        BulletNum = bulletNumber;
        _p_LanternLight.TriggerLightChange(bulletNumber);
        CanShoot = false;
    }

}
