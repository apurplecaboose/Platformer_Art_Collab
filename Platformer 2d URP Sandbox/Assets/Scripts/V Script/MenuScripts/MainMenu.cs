using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
    {
        SceneManager.LoadScene(SceneEnum.SceneList.scene_1.ToString());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
