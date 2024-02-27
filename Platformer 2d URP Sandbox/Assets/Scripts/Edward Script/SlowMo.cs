using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    float _sigma_DTime = 0; // used for lerp back to regular speed sum of u
    float _slowMoTimeScale = 0.2f;
    float _eric_loves_clash_from_r6_Timer;
    public AnimationCurve ReturntoNormalSpeedCurve; // for inspector use
    
    public float SlowMoResourceTime;
    public bool SlowMoToggle;
    void Update()
    {
        ///other script
        if(Input.GetKeyDown(KeyCode.N))
        {
            SlowMoToggle = !SlowMoToggle;
        }
        /// ^^^

        if (SlowMoResourceTime <= 0)
        {
            SlowMoToggle = false;
        }
        if(Time.timeScale == 1 && Time.fixedDeltaTime != 0.02f)
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
                //when no resource cannot activate sad mode
                activated = false;
                NeoTime(false);
                SlowMoToggle = false;
            }
            else
            {
                //when activated 
                SlowMoResourceTime -= Time.unscaledDeltaTime;
                _eric_loves_clash_from_r6_Timer = 0;
                NeoTime(true);
            }
        }
        if(!activated)
        {
            NeoTime(false);
            _eric_loves_clash_from_r6_Timer += Time.unscaledDeltaTime;

            float maxSlowMoResourceTime = 5f;
            if (SlowMoResourceTime < maxSlowMoResourceTime)
            {
                if(_eric_loves_clash_from_r6_Timer > 1f)
                {
                    SlowMoResourceTime += 0.5f * Time.unscaledDeltaTime;
                }
            }
            else
            {
                SlowMoResourceTime = maxSlowMoResourceTime; // catch case
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
            float returnTime = 0.45f;
            if (_sigma_DTime >= returnTime)
            {
                Time.timeScale = 1; // just in case not exactly 1
                Time.fixedDeltaTime = 0.02f; // set it to fixed time step
                return;
            }

            _sigma_DTime += Time.unscaledDeltaTime;
            float dynamicSlowMoTimeScale = UnscaledLerp(_slowMoTimeScale, returnTime, _sigma_DTime);
            Time.timeScale = dynamicSlowMoTimeScale; // copyed off of youtube
            Time.fixedDeltaTime = Time.deltaTime * dynamicSlowMoTimeScale; // copyed off of youtube


            float UnscaledLerp(float start, float lerpTime, float unscaledDeltaTime)
            {
                float lerpPercentage = unscaledDeltaTime / lerpTime;
                float output = Mathf.Lerp(start, 1, ReturntoNormalSpeedCurve.Evaluate(lerpPercentage));
                return output;
            }
        }
    }
}
