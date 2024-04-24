using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Master : MonoBehaviour
{
    public GameObject MiniBarrelPrefab;
    public Particle_Master ParticlePrefab;

    BoxCollider2D _collider;
    Vector2 _colliderSize;
    [SerializeField] int _SubBarrels;
    public float BarrelPower;
    public bool InvincibleBarrel;

    [HideInInspector] public GameObject P_Ref;
    [HideInInspector] public Rigidbody2D P_rb;
    [HideInInspector] public bool PlayerInRange;
    public float ExtraBlastUP;
    public Vector2 MinRange_MaxRange = new Vector2 (1,4);

    public float ParticleScale; 
    void Awake()
    {
        _collider = this.GetComponent<BoxCollider2D>();
        _colliderSize = _collider.size;
        
        if (_SubBarrels > 0) //check div/0 case
        {
            float leftBound = -_colliderSize.x / 2;
            float subIntervalSize = _colliderSize.x / _SubBarrels;
            float halfSubValue = subIntervalSize / 2;
            float firstSpawnPoint = leftBound + halfSubValue;
            for (int i = 0; i < _SubBarrels; i++)
            {
                GameObject subBarrel = Instantiate(MiniBarrelPrefab, this.transform, false); //instantiate in local space
                subBarrel.transform.localPosition = new Vector3(firstSpawnPoint + i * subIntervalSize, 0, 0); // set local position
                subBarrel.transform.localScale = new Vector3(subIntervalSize, 1, 1); //set local scale
                subBarrel.GetComponent<BoxCollider2D>().size = new Vector2(subBarrel.GetComponent<BoxCollider2D>().size.x, _colliderSize.y); //adjust y size to the same as parent
            }
        }
        else Debug.Log("Error: Divide by interval is <= 0");

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
