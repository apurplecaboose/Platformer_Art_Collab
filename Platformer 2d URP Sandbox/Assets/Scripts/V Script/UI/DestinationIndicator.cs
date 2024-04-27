using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationIndicator : MonoBehaviour
{//reference flag and camera, attatch

    public GameObject flag,indicator;//or destination whatever it is
    Transform flagTransform;
    // Start is called before the first frame update
    public GameObject[] Waypoints;
    float _orthoSize;//y
    float _screenHeight;
    float _screenWidth;//x
    float upRange;
    float _camUpBound, _camBotBound, _camLeftBound, _camRightBound;
    Vector3 _flagCamRelativePos;
    Color flagColor;


    Camera _cam;
    void Start()
    {
        _cam = this.GetComponent<Camera>();
        flag = GameObject.Find("flag");
        flagTransform = flag.transform;
        _flagCamRelativePos = flagTransform.InverseTransformPoint(transform.position);
        _screenWidth= (_orthoSize * (32 / 9)) / 2;
        _camLeftBound=- (_orthoSize * (32 / 9)) / 2;
        flagColor = flag.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_flagCamRelativePos);
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
            //indicator.transform.position = new Vector3(flag.transform.position.x, -_orthoSize+1);
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
    bool IsTargetOnScreen()
    {
        if (_flagCamRelativePos.x >_camLeftBound&&_flagCamRelativePos.x<_camRightBound)
        {
            if (_flagCamRelativePos.y > _camBotBound)
            {

                return true;
            }
            return true;
        }
        else
        {
            flagColor.a = 1;
            flag.GetComponent<SpriteRenderer>().color=flagColor;
            return false;
        }
    }
    //bool EdgeCheck()
    //{
    //    if (_flagCamRelativePos.x == 0) 
            
    //}
}
