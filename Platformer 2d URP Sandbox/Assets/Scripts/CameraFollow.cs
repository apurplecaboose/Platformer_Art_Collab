using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform _refToplayerTrans;
    // Start is called before the first frame update
    void Start()
    {
        _refToplayerTrans = GameObject.Find("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _refToplayerTrans.position + new Vector3(0, 0, -10);
    }
}
