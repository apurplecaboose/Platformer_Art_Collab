using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEATCODE : MonoBehaviour
{
    GameObject PlayerRef;
    AudioSource TriggerCheatsSFX;
    private void Awake()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        if (GameManager.CHEATS_ACTIVE )
        {
            TriggerCheatsSFX = this.GetComponent<AudioSource>();

            if(GameManager.CheatPosition == null)
            {
                GameManager.CheatPosition = PlayerRef.transform.position;
            }
            else
            {
                PlayerRef.transform.position = GameManager.CheatPosition.Value;
            }
            
        }
    }
    void Update()
    {
        if(GameManager.CHEATS_ACTIVE)
        {
            if(Input.GetKeyDown(KeyCode.Mouse2))
            {
                TriggerCheatsSFX.Play();
                Debug.Log("SetRespawnPoint" + PlayerRef.transform.position);
                GameManager.CheatPosition = PlayerRef.transform.position;
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                PlayerRef.GetComponent<P_ShootLogic>().ReloadFireBullets();
                TriggerCheatsSFX.Play();
            }

        }
    }
}
