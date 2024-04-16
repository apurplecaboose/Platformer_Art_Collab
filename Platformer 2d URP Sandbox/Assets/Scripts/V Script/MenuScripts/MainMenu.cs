using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu,LevelSelect,OptionsMenu;
    // Start is called before the first frame update
  public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    //public void LevelSelection()
    //{
    //    mainMenu.SetActive(false);
    //    LevelSelect.SetActive(true);
    //    SceneManager.LoadScene("Level Selection Scene");
    //}
    //public void Options()
    //{
    //    mainMenu.SetActive(false);
    //    OptionsMenu.SetActive(true);
    //    SceneManager.LoadScene("Options Scene");
    //}
    public void QuitGame()
    {
        Application.Quit();
    }
}
