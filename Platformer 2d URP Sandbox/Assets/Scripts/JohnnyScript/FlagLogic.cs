using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagLogic : MonoBehaviour
{
    float _anime_CountDown;
    PlayerControl _p_Script;
    [SerializeField] int _scene_Index;
    [SerializeField] bool _isWin;
    private void Awake()
    {
        _anime_CountDown = 3;
        _p_Script=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
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
            _anime_CountDown-=Time.deltaTime;
            if(_anime_CountDown < 0||Input.GetKeyDown(KeyCode.E))
            {
                //Switch Scene
                SceneManager.LoadScene(_scene_Index);
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
