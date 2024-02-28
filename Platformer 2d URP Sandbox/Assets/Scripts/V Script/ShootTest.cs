using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    public GameObject Bullet,player;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Shoot(player.transform.position,1.5f);
    }
    void Shoot(Vector3 dir, float firePower)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject BulletInstance = Instantiate(Bullet, player.transform.position,player.transform.rotation);
            //using GameObject BulletInstance to save the instance of object as variabl.(If no, the instantiate object is not asigned as gameobject in game ) 
            //������ñ����洢���ű��޷�������������Ϸ��������������б�̣�������Awake�а󶨵Ĳ��裩
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(dir * firePower, ForceMode2D.Impulse);
        }
    }
}
