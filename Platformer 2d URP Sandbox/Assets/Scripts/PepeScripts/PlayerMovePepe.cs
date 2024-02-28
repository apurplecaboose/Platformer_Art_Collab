using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePepe : MonoBehaviour
{
    Rigidbody2D _rb;
    float _horizontalInput;
    float _speed;
    float _jumpHight;
    float _force;
    public GameObject Prefeb;
    LanternLight _light;
    public int BulletNum;
    // Start is called before the first frame update
    void Start()
    {
        _light = GameObject.Find("LanternLight").GetComponent<LanternLight>();
        _rb = GetComponent<Rigidbody2D>();
        _speed = 4;
        _jumpHight = 7;
        _force = 5000;
        BulletNum = 4;

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(Prefeb, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = Vector2.up * _jumpHight;
            //_rb.AddForce(Vector2.up * _jumpHight,ForceMode2D.Force);

            _light.HasReduced = true;
        }

        _rb.velocity = new Vector2(_speed * _horizontalInput, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K key pressed.");
            _rb.AddForce(Vector2.right * _force, ForceMode2D.Force);
        }
    }
    private void FixedUpdate()
    {

    }
}
