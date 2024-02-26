using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    float _sigma_DTime = 0;
    float _slowMoTimeScale = 0.2f;
    public AnimationCurve ReturntoNormalSpeedCurve;
    public bool Temp;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            Temp = !Temp;
        }
        NeoTime(Temp);
    }



    public void NeoTime(bool toggle)
    {
        if(toggle)
        {
            Time.timeScale = _slowMoTimeScale;
            _sigma_DTime = 0;
        }
        else
        {
            ReturnFromNeoTime();
        }
    }
    public void ReturnFromNeoTime()
    {
        float UnscaledLerp(float start, float lerpTime, float unscaledDeltaTime)
        {
            float lerpPercentage = unscaledDeltaTime / lerpTime;
            float output = Mathf.Lerp(start, 1, ReturntoNormalSpeedCurve.Evaluate(lerpPercentage));
            return output;
        }

        _sigma_DTime += Time.unscaledDeltaTime;
        float returnTime = 1f;
        Time.timeScale = UnscaledLerp(_slowMoTimeScale, returnTime, _sigma_DTime);
        if(_sigma_DTime >= returnTime)
        {
            Time.timeScale = 1; // just in case not exactly 1

            //end function
        }
    }

}
