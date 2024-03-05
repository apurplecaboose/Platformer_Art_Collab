using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternLight : MonoBehaviour
{
    public Light2D Light;
    public AnimationCurve LightCurve;

    [SerializeField] Transform _SpriteMask;

    int _bulletCount;
    bool _startLightLerp;

    float _intensityStart, _intensityEnd;
    float _outRadiusStart, _outRadiusEnd;
    float _maskStart, _maskEnd;

    float _lerpDeltaTime = 0;
    float _lerpTime = 0.6f;

    public Vector3[] Intensity_Radius_Mask; //X component = Light Intensity float and Y component = Light Outer Radius float 
    private void Start()
    {
        Light = GetComponent<Light2D>();

        _intensityStart = Light.intensity;
        _outRadiusStart = Light.pointLightOuterRadius;
        _maskStart = _SpriteMask.localScale.x;
    }


    private void Update()
    {
        LightLevel();

    }
    /// <summary>
    /// To trigger lantern light change pass in the most recent bullet count and enjoy!!!
    /// </summary>
    /// <param name="bulletCount"></param>
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
                _maskStart = _SpriteMask.localScale.x;
                _startLightLerp = false;
                _lerpDeltaTime = 0;
            }

        }
        _intensityEnd = Intensity_Radius_Mask[_bulletCount].x;
        _outRadiusEnd = Intensity_Radius_Mask[_bulletCount].y;
        _maskEnd = Intensity_Radius_Mask[_bulletCount].z;

        Light.intensity = BasicFloatLerp(_intensityStart, _intensityEnd, _lerpTime, _lerpDeltaTime);
        Light.pointLightOuterRadius = BasicFloatLerp(_outRadiusStart, _outRadiusEnd, _lerpTime, _lerpDeltaTime);
        _SpriteMask.localScale = FloatToTransform(BasicFloatLerp(_maskStart, _maskEnd, _lerpTime, _lerpDeltaTime));
    }
    float BasicFloatLerp(float a, float b, float lerpTime, float dTime)
    {
        float lerpPercentage = dTime / lerpTime;
        float output = Mathf.Lerp(a, b, LightCurve.Evaluate(lerpPercentage));
        return output;
    }
    Vector3 FloatToTransform(float inputFloat)
    {
        return new Vector3(inputFloat, inputFloat, 1);
    }
}
