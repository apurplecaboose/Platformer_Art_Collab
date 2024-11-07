using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class StartMenuBGVideo : MonoBehaviour
{
    VideoPlayer _CutscenePlayer;
    public string PathURL_Filename_forWebGL;
    void Awake()
    {
        _CutscenePlayer = this.GetComponent<VideoPlayer>();
        _CutscenePlayer.source = VideoSource.Url; // double check
        string videoUrl = Application.streamingAssetsPath + "/" + PathURL_Filename_forWebGL;
        _CutscenePlayer.url = videoUrl;
    }
    void Update()
    {
        if(_CutscenePlayer.isPrepared)
        {
            _CutscenePlayer.Play();
        }
    }
}
