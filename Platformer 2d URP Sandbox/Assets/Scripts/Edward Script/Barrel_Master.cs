using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Master : MonoBehaviour
{
    public GameObject MiniBarrelPrefab;

    BoxCollider2D _collider;
    float _x_colliderSize;
    [SerializeField] int _SubBarrels;

    [HideInInspector] public GameObject P_Ref;
    [HideInInspector] public Rigidbody2D P_rb;
    public float BarrelPower;

    public bool PlayerInRange;

    void Awake()
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _x_colliderSize = _collider.size.x;
        if (_SubBarrels > 0) //check div/0 case
        {
            float leftBound = -_x_colliderSize / 2;
            float subIntervalSize = _x_colliderSize / _SubBarrels;
            float halfSubValue = subIntervalSize / 2;
            float firstSpawnPoint = leftBound + halfSubValue;
            for (int i = 0; i < _SubBarrels; i++)
            {
                GameObject subBarrel = Instantiate(MiniBarrelPrefab, new Vector3(firstSpawnPoint + i * subIntervalSize, 0, 0), Quaternion.identity, this.transform);
                subBarrel.transform.localScale = new Vector3(subIntervalSize, 1, 1);
            }
        }
        else Debug.Log("Error: Divide by interval > 0");

        P_Ref = GameObject.FindGameObjectWithTag("Player");
        P_rb = P_Ref.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInRange = true;//when enter the blast range player can be knocked back.

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInRange = false;//when leave the blast range player will not be knocked back.
        }
    }

}
