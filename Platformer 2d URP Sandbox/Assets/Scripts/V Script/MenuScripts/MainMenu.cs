using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneEnum.SceneList PlayTargetScene;
    [SerializeField] SceneEnum.SceneList Lv1Scene;
    [SerializeField] SceneEnum.SceneList Lv2Scene;
    [SerializeField] SceneEnum.SceneList Lv3Scene;
    [SerializeField] SceneEnum.SceneList HighScore;
    public void PlayGame()
    {
        SceneManager.LoadScene(PlayTargetScene.ToString());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Level1()
    {
        SceneManager.LoadScene(Lv1Scene.ToString());
    }
    public void Level2()
    {
        SceneManager.LoadScene(Lv2Scene.ToString());
    }
    public void Level3()
    {
        SceneManager.LoadScene(Lv3Scene.ToString());
    }
    public void SecretHighscore()
    {
        SceneManager.LoadScene(HighScore.ToString());
    }
}
