using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_ShootLogic : MonoBehaviour
{
    [SerializeField] float _firePower;
    private Vector3 _refToMousePosition;
    public Transform ShootDirection, ShootPoint;
    public List<GameObject> NewBullet;
    public int BulletIndex, BulletLimit;
    public GameObject Bullet;
    public bool HaveAmmo;

    // Update is called once per frame
    void Update()
    {
        Shoot(_firePower);
        print(_refToMousePosition);
        ReLoad();//Test Only
        _refToMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);//mouse input
    }

    void Shoot(float firePower)
    {
        Vector3 _dir = new Vector3(_refToMousePosition.x - ShootDirection.position.x, _refToMousePosition.y - ShootDirection.position.y);
        ShootDirection.up = _dir;// shooting direction

        if (BulletLimit > 0)
        {
            HaveAmmo = true;
        }
        else { HaveAmmo = false; }//Setting bullet limits

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveAmmo)
        {
            BulletLimit -= 1;//bullet limit set is 5
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            NewBullet.Add(BulletInstance);//record new bullet instantiate
            BulletIndex = NewBullet.Count - 1;
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(_dir * firePower, ForceMode2D.Impulse);
        }
    }

    void ReLoad()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletLimit = 5;
        }

    }
}
