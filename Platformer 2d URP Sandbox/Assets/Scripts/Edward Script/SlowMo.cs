using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    float _sigma_DTime = 0;
    float _slowMoTimeScale = 0.2f;
    public AnimationCurve ReturntoNormalSpeedCurve; // for inspector use
    
    public float SlowMoResourceTime;
    public bool SlowMoToggle;
    void Update()
    {
        ///other script
        if(Input.GetKeyDown(KeyCode.N))
        {
            SlowMoToggle = !SlowMoToggle;
            if (SlowMoToggle && SlowMoResourceTime >= 0)
            {
                //spam punishment
                float punishmentAmount = 0.5f;
                SlowMoResourceTime -= punishmentAmount; //public var
            }
        }
        /// ^^^

        if (SlowMoResourceTime <= 0)
        {
            SlowMoToggle = false;
        }
        if(Time.timeScale == 1)
        {
            Time.fixedDeltaTime = 0.02f; // set it to fixed time step 
            Debug.Log("Time.fixedDeltaTime Reset Catch Case!!!");
        }

        SlowMoReady(SlowMoToggle);
    }
    void SlowMoReady(bool activated)
    {
        if(activated)
        {
            if (SlowMoResourceTime <= 0) 
            {
                activated = false;
                NeoTime(false);
                SlowMoToggle = false;
            }
            else
            {
                SlowMoResourceTime -= Time.unscaledDeltaTime;
                NeoTime(true);
            }
        }
        if(!activated)
        {
            NeoTime(false);

            float maxSlowMoResourceTime = 5.5f;
            if (SlowMoResourceTime < maxSlowMoResourceTime)
            {
                SlowMoResourceTime += 0.5f * Time.unscaledDeltaTime;
            }
            else
            {
                SlowMoResourceTime = 5; // catch case
                Debug.Log("SlowMoResourceTime Overflow Catch Case!!!");
            }
        }
    }
    void NeoTime(bool startingOrStopping)
    {
        if(startingOrStopping)
        {
            Time.timeScale = _slowMoTimeScale;
            Time.fixedDeltaTime = Time.deltaTime * _slowMoTimeScale;
            _sigma_DTime = 0;
        }
        else
        {
            ReturnFromNeoTime();
        }
        void ReturnFromNeoTime()
        {
            float returnTime = 1f;
            if (_sigma_DTime >= returnTime)
            {
                Time.timeScale = 1; // just in case not exactly 1
                Time.fixedDeltaTime = 0.02f; // set it to fixed time step
                return;
            }

            _sigma_DTime += Time.unscaledDeltaTime;
            float dynamicSlowMoTimeScale = UnscaledLerp(_slowMoTimeScale, returnTime, _sigma_DTime);
            Time.timeScale = dynamicSlowMoTimeScale;
            Time.fixedDeltaTime = Time.deltaTime * dynamicSlowMoTimeScale;


            float UnscaledLerp(float start, float lerpTime, float unscaledDeltaTime)
            {
                float lerpPercentage = unscaledDeltaTime / lerpTime;
                float output = Mathf.Lerp(start, 1, ReturntoNormalSpeedCurve.Evaluate(lerpPercentage));
                return output;
            }
        }
    }



}
