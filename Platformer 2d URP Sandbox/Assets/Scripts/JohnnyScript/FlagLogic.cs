using System.Collections;
using System.Collections.Generic;
using TMPro;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagLogic : MonoBehaviour
{
    float _anime_CountDown;
    [SerializeField] SceneEnum.SceneList _TargetScene;
    bool _isWin, _trigger;
    TextMeshPro _endInfo;
    public TransitionProfile TransitionProfile;
    public Animator Panime;
    private void Start()
    {
        _anime_CountDown = 3;
        _endInfo = transform.GetChild(0).GetComponent<TextMeshPro>();//Get texmesh pro component as child of flag
    }

    void Update()
    {
        AnimationTimer();
    }
    void AnimationTimer()
    {
        if (_isWin)
        {
            Panime.Play("Win");
            GameManager.P_state = GameManager.PlayerState.Win;//J:limit player movement
            
            _anime_CountDown -= Time.unscaledDeltaTime;
            _endInfo.color += new Color(0, 0, 0, 0.25f * Time.unscaledDeltaTime);//show the instruction
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
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}

