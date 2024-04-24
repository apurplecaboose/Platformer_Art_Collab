using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            CollideWithKillBox();
        }
    }
    public void CollideWithKillBox()
    {
        GameManager.SceneCache = SceneEnum.ParseEnum<SceneEnum.SceneList>(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneEnum.SceneList.YouDied.ToString());
    }
}
