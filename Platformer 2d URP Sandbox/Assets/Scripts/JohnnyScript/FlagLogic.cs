using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagLogic : MonoBehaviour
{
    float _anime_CountDown;
    PlayerControl _p_Script;
    [SerializeField] Object _sceneFile; //load scene file as object from inspector
    [SerializeField] bool _isWin;
    TextMeshPro _endInfo;
    private void Awake()
    {
        _anime_CountDown = 3;
        _p_Script=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        _endInfo=GameObject.Find("EndInfo_Text").GetComponent<TextMeshPro>();    
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
            _anime_CountDown-=Time.deltaTime;
            _endInfo.color += new Color(0, 0, 0, 0.2f*Time.deltaTime);//show the instruction
            if (_anime_CountDown < 0||Input.GetKeyDown(KeyCode.Space))
            {
                //Switch Scene
                SceneManager.LoadScene(_sceneFile.name);
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
