using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TransitionsPlus;
using UnityEditor;

public class JohnnyLogo : MonoBehaviour
{
    public TransitionProfile profile;
    [SerializeField] SceneEnum.SceneList _TargetScene;
    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //SceneManager.LoadScene(1);
        TransitionAnimator.Start(profile, false, 0, _TargetScene.ToString(), LoadSceneMode.Single);
        print("sdklafj;dsklfjasdklfjalsdf");
    }
}
