using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfires : MonoBehaviour
{
    GameObject _refToPlayer;
    SlowMo _refToSlowMo;
    Rigidbody2D _p_rb;
    bool _isDash, _isLightingOn;
    [SerializeField] float _dashMultiplier;
    [SerializeField] Vector2 _MinandMaxCamfireForce;

    [SerializeField] Particle_Master _particlePrefab;
    [SerializeField] float _particlePrefabScale;
    private Color _SR_Color;
    SpriteRenderer _SR;
    // Update is called once per frame
    private void Awake()
    {
        AwakeRef();
        _SR_Color = _SR.color;
    }
    void AwakeRef()
    {
        _refToPlayer = GameObject.FindGameObjectWithTag("Player");
        _p_rb = _refToPlayer.GetComponent<Rigidbody2D>();
        _refToSlowMo = _refToPlayer.GetComponent<SlowMo>();
        _light = GetComponent<Light2D>();
        _SR = GetComponent<SpriteRenderer>();
        _insideFireParticle = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        _fireParticle = gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        _smokeParticle = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        _spriteLightRed = gameObject.transform.GetChild(3).GetComponent<Light2D>();
        _slowMoSpriteLight = gameObject.transform.GetChild(4).GetComponent<Light2D>();
    }
    private void Update()
    {
        if(_refToPlayer != null)
        {
            CampFiresTp();
            SlowMoVisible();
        }
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
                if (_light.pointLightOuterRadius < _turnOffParticleEmissionDueToOuterRadius1)//turn off inside fire particle emission when light is too small
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
    public AnimationCurve SpriteCurveStart;
    Light2D _light, _spriteLightRed, _slowMoSpriteLight;
    [SerializeField] float _intensityStart, _intensityPeak, _intensityEnd;
    [SerializeField] float _outRadiusStart, _outRadiusPeak;
    [SerializeField] float _s_intensityStart, _s_intensityPeak;
    float _lerpDeltaTime = 0;
    [SerializeField] float _timeA, _timeB, _timeC, _offSetTime;
    [SerializeField] float _turnOffParticleEmissionDueToOuterRadius1, _turnOffParticleEmissionDueToOuterRadius23;//1 for in insidefire, 23 for fire and smoke
    ParticleSystem _insideFireParticle, _fireParticle, _smokeParticle;
    /// <summary>
    /// light on the fire arter player shot campfire, Light lerps when hit just like fireball. Campfire is disabled for some amount of time (player can shoot through it) while the light lerps back to unlit on the campfire.
    /// </summary>
    void CampfireLight()
    {
        var insideFireParticleEmission = _insideFireParticle.emission;
        var frieParticleEmission = _fireParticle.emission;
        var smokeParticleEmission = _smokeParticle.emission;
        float offset = 0.01f;
        _lerpDeltaTime += Time.deltaTime;
        if (_lerpDeltaTime <= _timeA + offset)
        {
            _light.intensity = BasicFloatLerp(_intensityStart, _intensityPeak, _timeA, _lerpDeltaTime, CurveStart);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusPeak, _timeA, _lerpDeltaTime, CurveStart);
            _spriteLightRed.intensity = BasicFloatLerp(_s_intensityStart, _s_intensityPeak, _timeA, _lerpDeltaTime, SpriteCurveStart);
            insideFireParticleEmission.enabled = true; // turn on particle system
            frieParticleEmission.enabled = true;
            smokeParticleEmission.enabled = true;
        }
        else if (_lerpDeltaTime < _timeA + _timeB) ;//middle, flat peak, nothing happens
        else if (_lerpDeltaTime <= _timeA + _timeB + _timeC)
        {
            _light.intensity = BasicFloatLerp(_intensityPeak, _intensityEnd, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            _light.pointLightOuterRadius = BasicFloatLerp(_outRadiusPeak, _outRadiusStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), CurveEnd);
            //_spriteLightRed.intensity = BasicFloatLerp(_s_intensityPeak, _s_intensityStart, _timeC, _lerpDeltaTime - (_timeA + _timeB), SpriteCurveStart);
            if (_light.pointLightOuterRadius < _turnOffParticleEmissionDueToOuterRadius1)//turn off inside fire particle emission when light is too small
            {
                insideFireParticleEmission.enabled = false;
            }
            if (_light.pointLightOuterRadius < _turnOffParticleEmissionDueToOuterRadius23)//turn off fire and smoke particle emission when light is too small
            {
                frieParticleEmission.enabled = false;
                smokeParticleEmission.enabled = false;
            }
        }
        else if (_lerpDeltaTime > _timeA + _timeB + _timeC)//after all of light lerps, reset bool and timer
        {
            _spriteLightRed.intensity = 0;
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
    void SlowMoVisible()
    {
        if (Time.timeScale <= 0.2f)
        {//SlowMo Active
            _SR_Color.a = 1f;
            _SR.color = _SR_Color;
            _slowMoSpriteLight.enabled = true;
        }
        else if (Time.timeScale != 1)
        {//Deactivating
            float alphaLerp = Mathf.Lerp(1, 0, Time.timeScale);
            _SR_Color.a = alphaLerp;
            _SR.color = _SR_Color;
        }
        else
        {//deactive
            _SR_Color.a = 0f;
            _SR.color = _SR_Color;
            _slowMoSpriteLight.enabled = false;
        }
    }
}
