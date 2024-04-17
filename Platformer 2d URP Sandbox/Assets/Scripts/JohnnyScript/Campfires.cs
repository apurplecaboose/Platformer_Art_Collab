using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;

public class Campfires : MonoBehaviour
{
    GameObject _refToPlayer;
    SlowMo _refToSlowMo;
    Rigidbody2D _p_rb;
    [SerializeField] bool _isDash, _isLightingOn;
    [SerializeField] float _dashMultiplier;
    [SerializeField] Vector2 _MinandMaxCamfireForce;

    [SerializeField] Particle_Master _particlePrefab;
    [SerializeField] float _particlePrefabScale;
    // Update is called once per frame
    private void Awake()
    {
        //_dashMultiplier = 3f; //J;control dashing power //trash
        _refToPlayer = GameObject.FindGameObjectWithTag("Player");
        _p_rb = _refToPlayer.GetComponent<Rigidbody2D>();
        _refToSlowMo = _refToPlayer.GetComponent<SlowMo>();
        _light = GetComponent<Light2D>();
        _particleSystem = gameObject.transform.GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        CampFiresTp();//J: gather the function for campfire dashing in saperate script
                      //J;already save the campfire as profab,create variants prefab for different dashing power.
    }
    void CampFiresTp()
    {
        if (_isDash)
        {
            _refToSlowMo.SlowMoToggle = false;//J;return normal speed after shoot the campfire
            Vector2 dashDir;
            dashDir = transform.position - _refToPlayer.transform.position;
            dashDir.Normalize();
            float dashdistance = Vector3.Distance(transform.position, _refToPlayer.transform.position);
            dashdistance = Mathf.Clamp(dashdistance, _MinandMaxCamfireForce.x, _MinandMaxCamfireForce.y);
            if (Mathf.Sign(_p_rb.velocity.x) == Mathf.Sign(dashDir.x) && Mathf.Sign(_p_rb.velocity.y) == Mathf.Sign(dashDir.y))
            {
                //normal state
                _p_rb.AddForce(dashDir * dashdistance * _dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
                _isDash = false;
                return; // job well done solider return home!
            }
            if (Mathf.Sign(_p_rb.velocity.x) != Mathf.Sign(dashDir.x))
            {
                _p_rb.velocity = new Vector2(0, _p_rb.velocity.y); //cancel out x
            }
            if (Mathf.Sign(_p_rb.velocity.y) != Mathf.Sign(dashDir.y))
            {
                _p_rb.velocity = new Vector2(_p_rb.velocity.x, 0); //cancel out y
            }
            _p_rb.AddForce(dashDir * dashdistance * _dashMultiplier, ForceMode2D.Impulse); ///E: needs to be forcemode impulse
            _isDash = false;
        }
        if (_isLightingOn)
        {
            CampfireLight();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isLightingOn)// bullets can only shoot to campfire when it's not lighted on
        {
            if (collision.CompareTag("Bullet"))
            {
                New_OnLightingBurstParticles();

                _isLightingOn = true;
                _isDash = true;
                Destroy(collision.gameObject);
            }
        }
        else // if campfire is not finished burning but almost finished. let the player trigger the campfire early
        {
            if (collision.CompareTag("Bullet"))
            {
                if (_light.pointLightOuterRadius < _turnOffParticleEmissionDueToOuterRadius)//turn off particle emission when light is too small
                {
                    New_OnLightingBurstParticles();

                    _lerpDeltaTime = 0;
                    _isLightingOn = true;
                    _isDash = true;
                    Destroy(collision.gameObject);
                }
            }
        }
        void New_OnLightingBurstParticles()
        {
            Particle_Master Kachow = Instantiate(_particlePrefab, this.transform.position, Quaternion.identity);
            Kachow.transform.localScale = new Vector3(_particlePrefabScale, _particlePrefabScale, 0);
            Kachow.Lifetime = 0.75f;
        }
    }


    //CampFire Light
    public AnimationCurve CurveStart, CurveEnd;
    [SerializeField] Light2D _spriteLight;
    Light2D _light;
    [SerializeField] float _intensityStart, _intensityPeak, _intensityEnd;
    [SerializeField] float _outRadiusStart, _outRadiusPeak;
    [SerializeField] float _s_intensityStart, _s_intensityPeak;
    float _lerpDeltaTime = 0;
    [SerializeField] float _timeA, _timeB, _timeC, _offSetTime;
    [SerializeField] float _turnOffParticleEmissionDueToOuterRadius;
    ParticleSystem _particleSystem;
    /// <summary>
    /// light on the fire arter player shot campfire, Light lerps when hit just like fireball. Campfire is disabled for some amount of time (player can shoot through it) while the light lerps back to unlit on the campfire.
    /// </summary>
    void CampfireLight()
    {
        var emission = _particleSystem.emission;
        float offset = 0.01f;
        _lerpDeltaTime += Time.deltaTime;
        if (_lerpDeltaTime <= _timeA + offset)
        {
            _light.intensity = BasicFloatLerp(_intensityStart, _intensityPeak, _timeA, _lerpDeltaTime, CurveStart);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusPeak, _timeA, _lerpDeltaTime, CurveStart);
            _spriteLight.intensity = BasicFloatLerp(_s_intensityStart, _s_intensityPeak, _timeA, _lerpDeltaTime, CurveStart);
            emission.enabled = true; // turn on particle system
        }
        else if (_lerpDeltaTime < _timeA + _timeB)
        {
            //middle, flat peak, nothing happens
        }
        else if (_lerpDeltaTime <= _timeA + _timeB + _timeC)
        {
            _light.intensity = BasicFloatLerp(_intensityPeak, _intensityEnd, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusPeak, _outRadiusStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            _spriteLight.intensity = BasicFloatLerp(_s_intensityPeak, _s_intensityStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveStart);
            if (_light.pointLightOuterRadius < _turnOffParticleEmissionDueToOuterRadius)//turn off particle emission when light is too small
            {
                emission.enabled = false;
            }
        }
        else if (_lerpDeltaTime > _timeA + _timeB + _timeC)//after all of light lerps, reset bool and timer
        {
            _isLightingOn = false;
            _lerpDeltaTime = 0;
        }

        float BasicFloatLerp(float a, float b, float lerpTime, float dTime, AnimationCurve lerpCurve)
        {
            float lerpPercentage = dTime / lerpTime;
            float output = Mathf.Lerp(a, b, lerpCurve.Evaluate(lerpPercentage));
            return output;
        }
    }

}
