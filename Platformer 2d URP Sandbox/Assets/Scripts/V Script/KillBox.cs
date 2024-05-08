using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public POOF_MagicSmoke Poof;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //disable slow mo
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            collision.GetComponent<SlowMo>().SlowMoWinDisable();
            //cache current scene
            GameManager.SceneCache = SceneEnum.ParseEnum<SceneEnum.SceneList>(SceneManager.GetActiveScene().name);
            //instantiate poof prefab and link player gameobject
            POOF_MagicSmoke poof = Instantiate(Poof, collision.transform.position, Quaternion.identity);
            poof.Player = collision.gameObject;
        }
    }
}
