using System.Collections;
using System.Collections.Generic;
using TMPro;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagLogic : MonoBehaviour
{
    float _anime_CountDown;
    PlayerControl _p_Script;

    [SerializeField] SceneEnum.SceneList _TargetScene;
    bool _isWin, _trigger;
    TextMeshPro _endInfo;
    public TransitionProfile TransitionProfile;
    private void Start()
    {
        _anime_CountDown = 3;
        _p_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        _endInfo = transform.GetChild(0).GetComponent<TextMeshPro>();//Get texmesh pro component as child of flag
    }

    // Update is called once per frame
    void Update()
    {
        AnimationTimer();
    }
    void AnimationTimer()
    {
        if (_isWin)
        {
            _p_Script.RefPlayerState = PlayerControl.PlayerState.Win;//J:limit player movement
            
            _anime_CountDown -= Time.deltaTime;
            _endInfo.color += new Color(0, 0, 0, 0.25f * Time.deltaTime);//show the instruction
            if (_anime_CountDown < 0 || Input.GetKeyDown(KeyCode.Space))
            {
                //Switch Scene
                if (!_trigger)
                {
                    TransitionAnimator.Start(TransitionProfile, false, 0, _TargetScene.ToString());
                    _trigger = true;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isWin = true;
        }
    }
}

