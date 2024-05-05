using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_Misc : MonoBehaviour
{
    [SerializeField] SceneEnum.SceneList _MenuMainScene;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(_MenuMainScene.ToString());
        }
    }
}
