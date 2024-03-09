using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletLight : MonoBehaviour
{

    public AnimationCurve CurveStart, CurveEnd;
    public Light2D Light;
    P_Projectile_Pepe _p_Projectile;

    [SerializeField] float _intensityStart, _intensityPeak, _intensityEnd;
    [SerializeField] float _outRadiusStart, _outRadiusPeak, _outRadiusEnd;

    float _lerpDeltaTime = 0;
    [SerializeField] float _timeA, _timeB, _timeC;

    private void Awake()
    {
        _p_Projectile = transform.parent.gameObject.GetComponent<P_Projectile_Pepe>();
        Light = GetComponent<Light2D>();
    }
    //void Start()
    //{
    //    _intensityStart = Light.intensity;
    //    _outRadiusStart = Light.pointLightOuterRadius;
    //}

    // Update is called once per frame
    void Update()
    {
        //if (!_p_Projectile.IsGrounded)
        //{

        //}
        //if (_p_Projectile.IsGrounded)
        //{
        //    EnlargeLight();
        //    Debug.Log("Grounded");
        //}
    }

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
            Light.intensity = BasicFloatLerp(_intensityPeak, 0.5f, _timeC, _lerpDeltaTime-(_timeA + _timeB), CurveEnd);
            Light.pointLightOuterRadius = BasicFloatLerp(_outRadiusPeak, _outRadiusStart, _timeC, _lerpDeltaTime-(_timeA + _timeB), CurveEnd);
            Debug.Log("Decreasing");
        }
        if (_lerpDeltaTime >= _timeA + _timeB + _timeC)
        {
            
            //destroy times up
        }

    }

    float BasicFloatLerp(float a, float b, float lerpTime, float dTime, AnimationCurve lerpCurve)
    {
        float lerpPercentage = dTime / lerpTime;
        float output = Mathf.Lerp(a, b, lerpCurve.Evaluate(lerpPercentage));
        return output;
    }
}
