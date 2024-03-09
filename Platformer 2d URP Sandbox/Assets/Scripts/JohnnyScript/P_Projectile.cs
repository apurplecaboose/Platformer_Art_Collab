using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class P_Projectile : MonoBehaviour
{
    //changed var names to fit naming convention
    [SerializeField] float _firePower, _bulletRange, _bulletTimer;
    float _mollyTime;
    GameObject _p_Ref;
    Rigidbody2D _rb;

    public Vector2 BlastDir;
    public Vector3 ShootDir;

    bool _isGrounded = false;
    public Vector3 PlayerIntialPosition;
    //bulletLight

    private void Awake()
    {
        //E: initalized all references on awake
        _p_Ref = GameObject.FindGameObjectWithTag("Player"); //E: changed to player tag therefore name of gameobject doesnt matter
        _rb = this.GetComponent<Rigidbody2D>();

        //BulletShooting(_firePower);

        Light = GetComponent<Light2D>();
        _mollyTime = _timeA + _timeB + _timeC - _offSetTime;
    }
    private void Start()
    {
        _rb.AddForce(ShootDir.normalized * _firePower, ForceMode2D.Impulse);
    }

    private void Update()
    {
        DestroyBullet();//Destroy bullets
        if (_isGrounded)
        {
            //light stuff
            EnlargeLight();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectGround(collision);
        if (!_isGrounded)
        {
            DestroyByOtherBullets(collision);
        }
    }

    void DetectGround(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
            _rb.bodyType = RigidbodyType2D.Static;
            _bulletTimer = _mollyTime;//bullets will exist extra long when they collide with grounds or walls
        }
    }

    void DestroyByOtherBullets(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        _bulletTimer -= Time.deltaTime;
        float bulletDistance = Vector3.Distance(this.transform.position, PlayerIntialPosition);
        if (bulletDistance > _bulletRange) //destroy bullet when it gets to certain range
        {
            Destroy(gameObject);
        }
        else if (_bulletTimer <= 0)//destroy bullet when it lasts certain time
        {
            Destroy(gameObject);
        }
    }

    //Enlarge Light Function Varibles
    public AnimationCurve CurveStart, CurveEnd;
    Light2D Light;

    [SerializeField] float _intensityStart, _intensityPeak, _intensityEnd;
    [SerializeField] float _outRadiusStart, _outRadiusPeak;

    float _lerpDeltaTime = 0;
    [SerializeField] float _timeA, _timeB, _timeC, _offSetTime;
    void EnlargeLight()
    {
        float offset = 0.01f;
        _lerpDeltaTime += Time.deltaTime;
        if (_lerpDeltaTime <= _timeA + offset)
        {
            Light.intensity = BasicFloatLerp(_intensityStart, _intensityPeak, _timeA, _lerpDeltaTime, CurveStart);
            Light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusPeak, _timeA, _lerpDeltaTime, CurveStart);
        }
        else if (_lerpDeltaTime < _timeA + _timeB)
        {
            //middle, flat peak, nothing happens
        }
        else if (_lerpDeltaTime <= _timeA + _timeB + _timeC)
        {
            Light.intensity = BasicFloatLerp(_intensityPeak, _intensityEnd, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            Light.pointLightOuterRadius = BasicFloatLerp(_outRadiusPeak, _outRadiusStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
        }

        float BasicFloatLerp(float a, float b, float lerpTime, float dTime, AnimationCurve lerpCurve)
        {
            float lerpPercentage = dTime / lerpTime;
            float output = Mathf.Lerp(a, b, lerpCurve.Evaluate(lerpPercentage));
            return output;
        }
    }
}
