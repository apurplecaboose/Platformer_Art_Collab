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
    public float LightIntensity,CurrentLightIntensity, TargetLightIntensity;
    public float LightOuterRadius, CurrentLightOuterRadius, TargetLightOuterRadius;
    public float FalloffStrength, CurrentFalloffStrength, TargetFalloffStrength;
    public bool HasReduced;
    public float Timer, LerpTime, T, StartLerp;
    public AnimationCurve LightCurve;

    private void Start()
    {
        Light = GetComponent<Light2D>();
        LerpTime = 3;
        LightIntensity = Light.intensity;
        LightOuterRadius = Light.pointLightOuterRadius;
        FalloffStrength = Light.falloffIntensity;
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
        TargetLightIntensity = LightIntensity - 0.25f;

        if (HasReduced)
        {
            Timer += Time.deltaTime;

            //lightLerp();

            T = Timer / LerpTime;
            CurrentLightIntensity = Mathf.Lerp(LightIntensity, TargetLightIntensity, T);
            Light.intensity = CurrentLightIntensity;
            LightIntensity = CurrentLightIntensity;
            HasReduced = false;
        }


        //LightOuterRadius -= 0.5f;
        //Light.pointLightOuterRadius = LightOuterRadius;

        //FalloffStrength += 0.05f;
        //Light.falloffIntensity = FalloffStrength;

        //HasReduced = true;
        

    }
    float _lerpDeltaTime = 0;
    float BasicFloatLerp(float a, float b, float lerpTime, float dTime)
    {
        float lerpPercentage = dTime / lerpTime;
        float output = Mathf.Lerp(a, b, LightCurve.Evaluate(lerpPercentage));
        return output;
    }
}
