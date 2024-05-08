using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_Misc : MonoBehaviour
{
    [SerializeField] SceneEnum.SceneList _MenuMainScene;
    public POOF_MagicSmoke Poof;
    public static Reset_Misc instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Debug.Log("2 of these bitches in the scene" + this.gameObject);
    }
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        if (Input.GetKeyDown(KeyCode.F))
        {
            POOF_MagicSmoke poof = Instantiate(Poof, this.transform.position, Quaternion.identity);
            poof.Player = this.gameObject;
            poof.Respawn = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(_MenuMainScene.ToString());
        }
    }
}
