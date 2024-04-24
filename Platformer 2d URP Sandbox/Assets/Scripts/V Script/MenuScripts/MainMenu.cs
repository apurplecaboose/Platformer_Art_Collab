using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneEnum.SceneList PlayTargetScene;
  public void PlayGame()
    {
        SceneManager.LoadScene(PlayTargetScene.ToString());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
