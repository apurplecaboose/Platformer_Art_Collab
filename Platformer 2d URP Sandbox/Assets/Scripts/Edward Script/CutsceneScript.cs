using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public VideoPlayer CutscenePlayer;
    [SerializeField] SceneEnum.SceneList _TargetScene;
    public TransitionProfile TransitionProfile;
    [SerializeField] float _videoOffset;
    [SerializeField] int _cutsceneNumber;
    [SerializeField] GameObject SkipUI;
    [SerializeField] Image Dial;
    [SerializeField] GameObject CHEATERSkipUI;

    float _cutsceneLength, _cutsceneTimer, _skipTimer, _skipcutscenebuttonholdtime = 1.25f;
    bool _toggle;
    public string VIDEOPATH;
    private void Awake()
    {
        CutscenePlayer.source = VideoSource.Url;
        string videoUrl = Application.streamingAssetsPath + "/" + VIDEOPATH;
        CutscenePlayer.url = videoUrl;
    }
    private void Start()
    {
        switch (_cutsceneNumber)
        {
            case 2:
                GameManager.Lv1Cleared = true;
                break;
            case 3:
                GameManager.Lv2Cleared = true;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if(CutscenePlayer.isPrepared)
        {
            _cutsceneLength = (float)CutscenePlayer.length;
            if (_cutsceneTimer < _videoOffset)
            {
                Debug.Log("Offset is larger than video length u idiot");
            }
            _cutsceneTimer += Time.deltaTime;
            if (_cutsceneTimer >= _cutsceneLength - _videoOffset)
            {
                if (!_toggle)
                {
                    TransitionAnimator.Start(TransitionProfile, false, 0, _TargetScene.ToString(), LoadSceneMode.Single);
                    _toggle = true;
                }
            }
        }

        if(!_toggle)
        {
            HoldToSkipCutscene();
        }
        else
        {
            SkipUI.SetActive(false);
        }
     
        if(_cutsceneTimer > 5f)
        {
            CHEATERSkipUI.SetActive(false);
        }
    }
    void HoldToSkipCutscene()
    {
        if (Input.GetKey(KeyCode.Space))
        {// allow player to skip cutscene if hold
            _skipTimer += Time.deltaTime;
            if (_skipTimer > _skipcutscenebuttonholdtime)
            {
                TransitionAnimator.Start(TransitionProfile, false, 0, _TargetScene.ToString(), LoadSceneMode.Single);
                _toggle = true;
            }
        }
        else
        {
            if (_skipTimer > 0)
            {
                _skipTimer -= 0.5f * Time.deltaTime;
            }
        }
        if (_skipTimer > 0)
        {
            SkipUI.SetActive(true);
            Dial.fillAmount = Mathf.InverseLerp(0, _skipcutscenebuttonholdtime, _skipTimer);
        }
        else
        {
            SkipUI.SetActive(false);
        }
    }
}
