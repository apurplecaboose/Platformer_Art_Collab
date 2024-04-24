using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; //<--- added this
using TMPro;
using Cinemachine;

public class SlowMo : MonoBehaviour
{
    float _sigma_DTime = 0; // used for lerp back to regular speed sum of u
    float _slowMoTimeScale = 0.2f;
    float _defaultPhysicsTimestep = 0.02f;
    float _eric_loves_clash_from_r6_Timer;
    public AnimationCurve ReturntoNormalSpeedCurve; // for inspector use
    public float SlowMoResourceTime = 7f;
    float _maxSlowMoTime;
    [SerializeField] float _rechargeRate = 1.5f, _Clash_From_r6_cooldown = 1f;
    public bool SlowMoToggle;
    TMP_Text _Slowmo_Text;
    Volume _JojoTimeVolume, _JojoTimeVolume2;

    GameObject _SlowMoCanvas;
    private void Awake()
    {
        _SlowMoCanvas = Camera.main.transform.GetChild(1).gameObject;
        _Slowmo_Text = _SlowMoCanvas.transform.GetChild(0).GetComponent<TMP_Text>();
        GameObject CM_VirtualCamRef = Camera.main.transform.GetChild(0).gameObject;
        _JojoTimeVolume = CM_VirtualCamRef.GetComponents<Volume>()[0];
        _JojoTimeVolume2 = CM_VirtualCamRef.GetComponents<Volume>()[1];
        _maxSlowMoTime = SlowMoResourceTime; // set time in script as it is editable in inspector
    }
    void Update()
    {
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

        _Slowmo_Text.text = SlowMoResourceTime.ToString("0.00");

        if(GameManager.P_state == GameManager.PlayerState.Win)
        {
            SlowMoWinDisable();
        }
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
                _SlowMoCanvas.SetActive(false); //turn off ui
                _JojoTimeVolume.weight = 0;
                _JojoTimeVolume2.weight = 0;
            }
            else
            {
                //when activated 
                SlowMoResourceTime -= Time.unscaledDeltaTime;
                _eric_loves_clash_from_r6_Timer = 0;
                NeoTime(true);
                _SlowMoCanvas.SetActive(true); //turn off ui
                if(_JojoTimeVolume.weight <= 1)
                {
                    _JojoTimeVolume.weight += 10 * Time.unscaledDeltaTime;
                }
                _JojoTimeVolume2.weight = 1 - SlowMoResourceTime / _maxSlowMoTime;
            }
        }
        if(!activated)
        {
            _SlowMoCanvas.SetActive(false); //turn off ui
            _JojoTimeVolume.weight = 0;
            _JojoTimeVolume2.weight = 0;
            NeoTime(false);
            _eric_loves_clash_from_r6_Timer += Time.unscaledDeltaTime;

            if (SlowMoResourceTime < _maxSlowMoTime)
            {
                if(_eric_loves_clash_from_r6_Timer > _Clash_From_r6_cooldown)
                {
                    SlowMoResourceTime += _rechargeRate * Time.unscaledDeltaTime;
                }
            }
            else
            {
                SlowMoResourceTime = _maxSlowMoTime; // catch case
            }
        }
    }
    void NeoTime(bool startingOrStopping)
    {
        if(startingOrStopping)
        {
            Time.timeScale = _slowMoTimeScale;
            Time.fixedDeltaTime = _defaultPhysicsTimestep * Time.timeScale; // read unity scripting API
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
                Time.fixedDeltaTime = _defaultPhysicsTimestep * Time.timeScale; // set it to fixed time step
                return;
            }

            _sigma_DTime += Time.unscaledDeltaTime;
            float dynamicSlowMoTimeScale = UnscaledLerp(_slowMoTimeScale, returnTime, _sigma_DTime);
            Time.timeScale = dynamicSlowMoTimeScale; 
            Time.fixedDeltaTime = _defaultPhysicsTimestep * Time.timeScale;


            float UnscaledLerp(float start, float lerpTime, float unscaledDeltaTime)
            {
                float lerpPercentage = unscaledDeltaTime / lerpTime;
                float output = Mathf.Lerp(start, 1, ReturntoNormalSpeedCurve.Evaluate(lerpPercentage));
                return output;
            }
        }
    }

    public void SlowMoWinDisable()
    {
        SlowMoReady(false);//set to off

        //just in case
        _SlowMoCanvas.SetActive(false);
        _JojoTimeVolume.weight = 0;
        _JojoTimeVolume2.weight = 0;

        //disable script
        this.enabled = false;
    }
}
