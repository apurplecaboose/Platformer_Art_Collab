using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class JohnnyLogo : MonoBehaviour
{
    VideoPlayer video;
    void Awake()
    {
#if UNITY_WEBGL

            SceneManager.LoadScene(1);

            //video.source = VideoSource.Url;
            //Debug.Log("USE URL");1
            //string videoUrl = Application.streamingAssetsPath + "/" + "TESTVIDEO" + ".mp4";
            //video.url = videoUrl;
            //video.Play();
            //video.loopPointReached += CheckOver;
#else
        video = GetComponent<VideoPlayer>();
        video.source = VideoSource.VideoClip;
        Debug.Log("USE VideoClip");
        video.Play();
        video.loopPointReached += CheckOver;
#endif
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }
}
