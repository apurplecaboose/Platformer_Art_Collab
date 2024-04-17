using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class P_Projectile : MonoBehaviour
{
    //changed var names to fit naming convention
    [SerializeField] float _firePower, _bulletRange, _bulletTimer;
    float _mollyTime;
    Rigidbody2D _rb;

    public Vector2 BlastDir;
    public Vector3 ShootDir;

    bool _isGrounded = false;
    [HideInInspector]
    public Vector3 PlayerIntialPosition;

    ParticleSystem _particleSystem;

    private void Awake()
    {
        //E: initalized all references on awake
        _rb = this.GetComponent<Rigidbody2D>();

        //BulletShooting(_firePower);

        _light = GetComponent<Light2D>();
        _mollyTime = _timeA + _timeB + _timeC - _offSetTime;

        _particleSystem = GetComponent<ParticleSystem>();
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
        if (collision.collider.CompareTag("Ground"))
        {
            var particleGravity = _particleSystem.main;
            particleGravity.gravityModifier = -1;//change particleGravity after bullet got on the ground
            _isGrounded = true;
            _rb.bodyType = RigidbodyType2D.Static;
            _bulletTimer = _mollyTime;//bullets will exist extra long when they collide with grounds or walls
        }

        if (!_isGrounded) // if currently 
        {
            if (collision.collider.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
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
    Light2D _light;

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
            _light.intensity = BasicFloatLerp(_intensityStart, _intensityPeak, _timeA, _lerpDeltaTime, CurveStart);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusPeak, _timeA, _lerpDeltaTime, CurveStart);
        }
        else if (_lerpDeltaTime < _timeA + _timeB)
        {
            //middle, flat peak, nothing happens
        }
        else if (_lerpDeltaTime <= _timeA + _timeB + _timeC)
        {
            _light.intensity = BasicFloatLerp(_intensityPeak, _intensityEnd, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusPeak, _outRadiusStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
        }

        float BasicFloatLerp(float a, float b, float lerpTime, float dTime, AnimationCurve lerpCurve)
        {
            float lerpPercentage = dTime / lerpTime;
            float output = Mathf.Lerp(a, b, lerpCurve.Evaluate(lerpPercentage));
            return output;
        }
    }
}
