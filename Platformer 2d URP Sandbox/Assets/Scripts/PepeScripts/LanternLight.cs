using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using System.Threading;
using Unity.VisualScripting;

public class LanternLight : MonoBehaviour
{
    public Light2D Light;
    public AnimationCurve LightCurve;

    int _bulletCount;
    bool _startLightLerp;

    float _intensityStart, _intensityEnd;
    float _outRadiusStart, _outRadiusEnd;

    float _lerpDeltaTime = 0;
    float _lerpTime = 0.6f;

    public Vector2[] Intensity_Radius; //X component = Light Intensity float and Y component = Light Outer Radius float 
    private void Start()
    {
        Light = GetComponent<Light2D>();

        _intensityStart = Light.intensity;
        _outRadiusStart = Light.pointLightOuterRadius;
    }

    private void Update()
    {
        LightLevel();

    }
    public void TriggerLightChange(int bulletCount)
    {
        _startLightLerp = true;
        _bulletCount = bulletCount;
    }
    void LightLevel()
    {
        if (_startLightLerp)
        {
            _lerpDeltaTime += Time.deltaTime;
            if(_lerpDeltaTime >= _lerpTime)
            {
                _intensityStart = Light.intensity;
                _outRadiusStart = Light.pointLightOuterRadius;
                _startLightLerp = false;
                _lerpDeltaTime = 0;
            }

        }
        _intensityEnd = Intensity_Radius[_bulletCount].x;
        _outRadiusEnd = Intensity_Radius[_bulletCount].y;

        Light.intensity = BasicFloatLerp(_intensityStart, _intensityEnd, _lerpTime, _lerpDeltaTime);
        Light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusEnd, _lerpTime, _lerpDeltaTime);
    }
    float BasicFloatLerp(float a, float b, float lerpTime, float dTime)
    {
        float lerpPercentage = dTime / lerpTime;
        float output = Mathf.Lerp(a, b, LightCurve.Evaluate(lerpPercentage));
        return output;
    }
}
