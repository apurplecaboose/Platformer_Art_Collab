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
    public bool HasReduced;
    public float Timer, LerpTime, T, StartLerp;
    public AnimationCurve LightCurve;
    public PlayerMovePepe _refToPlayerSC;

    float _lerpDeltaTime = 0;
    public float[] LightIntensityValue;
    public float[] LightOuterRadiusValue;
    public float[] FalloffStrengthValue;
    private void Start()
    {
        Light = GetComponent<Light2D>();
        LerpTime = 3;


        LightIntensityValue[0] = 1f;
        LightOuterRadiusValue[0] = 14f;
        FalloffStrengthValue[0] = 0.6f;

        LightIntensityValue[1] = 0.8f;
        LightOuterRadiusValue[1] = 11f;
        FalloffStrengthValue[1] = 0.65f;

        LightIntensityValue[2] = 0.6f;
        LightOuterRadiusValue[2] = 9.5f;
        FalloffStrengthValue[2] = 0.7f;

        LightIntensityValue[3] = 0.5f;
        LightOuterRadiusValue[3] = 7f;
        FalloffStrengthValue[3] = 0.75f;

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {// use bool to start timer
            _lerpDeltaTime += Time.deltaTime;
        }
        Light.intensity = BasicFloatLerp(5, 0.1f, 5, _lerpDeltaTime);

        Light.pointLightOuterRadius = BasicFloatLerp(14, 5, 5, _lerpDeltaTime);

        Light.falloffIntensity = BasicFloatLerp(0.6f, 1, 5, _lerpDeltaTime);
    }

    public void LightLevel()
    {
        if (HasReduced)
        {
            _lerpDeltaTime += Time.deltaTime;
            if(_lerpDeltaTime >= 0.6f)
            {
                HasReduced = false;
                _lerpDeltaTime = 0;
            }

        }

        for (int i = 0; i<_refToPlayerSC.BulletNum; i++)
        {

        }
        //LightOuterRadius -= 0.5f;
        //Light.pointLightOuterRadius = LightOuterRadius;

        //FalloffStrength += 0.05f;
        //Light.falloffIntensity = FalloffStrength;

        //HasReduced = true;
        

    }

    float BasicFloatLerp(float a, float b, float lerpTime, float dTime)
    {
        float lerpPercentage = dTime / lerpTime;
        float output = Mathf.Lerp(a, b, LightCurve.Evaluate(lerpPercentage));
        return output;
    }
}
