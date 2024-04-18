using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationIndicator : MonoBehaviour
{//reference flag and camera, attatch

    public GameObject flag;//or destination whatever it is
    Transform flagTransform;
    // Start is called before the first frame update
    public GameObject[] Waypoints;
    float _orthoSize;
    float _screenHeight;
    float _screenWidth;
    float upRange;


    Camera _cam;
    void Start()
    {
        _cam = this.GetComponent<Camera>();
        flag = GameObject.Find("flag");
        flagTransform = flag.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //ArrowBehavior();
        screensize();
    }
    void ArrowBehavior()
    {
        if (flagTransform != null)
        {
            Vector2 direction = flagTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void screensize()
    {
        _orthoSize = _cam.orthographicSize;
        float screenWidthOffset = (_orthoSize * (32 / 9))/2;
        float m =flag.transform.position.y/flag.transform.position.x;
        Waypoints[0].transform.position = this.transform.position + new Vector3(screenWidthOffset, _orthoSize, 0);//some position;
        Waypoints[1].transform.position = transform.position+ new Vector3(-screenWidthOffset,_orthoSize,0);
        //Waypoints[2].transform.position = transform.position+new Vector3(screenWidthOffset,_;
        //Waypoints[3].transform.position = //some position;
        //Waypoints[2].transform.position = transform.position+new Vector3(screenWidthOffset)
        //check up and down
        if (flag.transform.position.y > transform.position.y + _orthoSize)
        {
            Debug.Log("its above me");
        }
        else if (flag.transform.position.y < transform.position.y - _orthoSize)
        {
            Debug.Log("its below me");
        }
        if (flag.transform.position.x > transform.position.x + screenWidthOffset)
        {
            Debug.Log("its on my right");
        }
        else if (flag.transform.position.y < transform.position.y - screenWidthOffset)
        {
            Debug.Log("its on my left");
        }

    }
}
