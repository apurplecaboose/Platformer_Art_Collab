using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TransitionsPlus;
using Cinemachine;
using UnityEngine.SceneManagement;

public class POOF_MagicSmoke : MonoBehaviour
{
    [HideInInspector]public bool Respawn;
    Light2D _oldLight, _replacementLight;
    [HideInInspector] public GameObject Player;

    public TransitionProfile TransitionProfile;
    private void Awake()
    {
        //set new camera target
        Camera.main.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Follow = this.gameObject.transform;
        
        if(Player == null) Player = GameObject.FindGameObjectWithTag("Player");
        if(Time.timeScale != 1 && Time.fixedDeltaTime != 0.02f)// reset slow mo if in slow mo
        {
            Time.timeScale = 1; 
            Time.fixedDeltaTime = 0.02f;
            Player.GetComponent<SlowMo>().SlowMoWinDisable();
        }
        _replacementLight = this.GetComponent<Light2D>();
        _oldLight = Player.GetComponent<P_ShootLogic>()._p_LanternLight.Light; //bad way to cache maybe improve in the future
        ReplaceLight2D(_oldLight, _replacementLight); // replace light so player can see
    }
    private void Start()
    {
        Destroy(Player); //No need for player now all the data has been ripped clean
        Invoke("StartTransition", 0.35f); // wait for poof animation to finish playing before crossfading in fade to black
    }
    void StartTransition()
    {
        if (Respawn) TransitionAnimator.Start(TransitionProfile, false, 0, SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        else TransitionAnimator.Start(TransitionProfile, false, 0, SceneEnum.SceneList.YouDied.ToString(), LoadSceneMode.Single);
    }
    void ReplaceLight2D(Light2D old, Light2D replacement)
    {
        replacement.intensity = old.intensity;
        replacement.intensity = Mathf.Clamp(replacement.intensity, 1.5f, replacement.intensity);
        replacement.pointLightOuterRadius = old.pointLightOuterRadius;
        replacement.pointLightOuterRadius = Mathf.Clamp(replacement.pointLightOuterRadius, 5f, replacement.pointLightOuterRadius);
        replacement.falloffIntensity = old.falloffIntensity;
        replacement.color = old.color;
    }
}
