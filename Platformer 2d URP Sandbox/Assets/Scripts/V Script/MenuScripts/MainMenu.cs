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
    int secretbuttoncount;
    public AudioSource Active, Deactive;
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
    public void SecretCheatButton()
    {
        if(GameManager.CHEATS_ACTIVE)
        {
            GameManager.CHEATS_ACTIVE = false;
            Debug.Log("Cheats DEACTIVE");
            Deactive.Play();
        }
        else
        {
            secretbuttoncount += 1;
            if(secretbuttoncount == 7)
            {
                secretbuttoncount = 0;
                Active.Play();
                GameManager.CHEATS_ACTIVE = true;
                Debug.Log("Cheats ACTIVE");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
