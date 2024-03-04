using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFollow : MonoBehaviour
{
    public Transform FollowTarget;
    [SerializeField] LanternLight LightScriptRef;
    [SerializeField] int BulletCount = 3;
    void Update()
    {
        this.transform.position = FollowTarget.position;
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            BulletCount--;
            LightScriptRef.TriggerLightChange(BulletCount);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            BulletCount++;
            LightScriptRef.TriggerLightChange(BulletCount);
        }
    }
}
