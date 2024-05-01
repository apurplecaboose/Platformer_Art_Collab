using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationIndicator : MonoBehaviour
{//attach on _flag

    public GameObject _flag,_indicator;//or destination whatever it is
    Transform flagTransform;
    // Start is called before the first frame update
    //public GameObject[] Waypoints;
    float _orthoSize;//y
    float _screenHeight;
    float _screenWidth;//x
    float upRange;
    float _camUpBound, _camBotBound, _camLeftBound, _camRightBound;
    Vector3 _flagCamRelativePos;
   [SerializeField]Color _indicatorColor;


    Camera _cam;
    void Start()
    {
        _cam = Camera.main;
        _flag = this.gameObject;
        flagTransform = _flag.transform;
        _flagCamRelativePos = transform.InverseTransformPoint(_cam.transform.position);
        _orthoSize = _cam.orthographicSize;
        _screenWidth= (_orthoSize * (32 / 9)) / 2;
        _camRightBound = (_orthoSize * (32 / 9)) / 2;
        _camLeftBound = -_camRightBound;
        _indicator = gameObject.transform.GetChild(0).gameObject;
        _indicatorColor = _indicator.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        _flagCamRelativePos = transform.InverseTransformPoint(_cam.transform.position);
        Debug.Log(_flagCamRelativePos);
        Debug.Log(_camLeftBound);
        //ArrowRotation();
        //screensize();
        IsTargetOnScreen();
        EdgeCheck();
    }
    void ArrowRotation()
    {
        if (flagTransform != null)
        {
            Vector2 direction = flagTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    //void screensize()
    //{
    //    _orthoSize = _cam.orthographicSize;
    //    float screenWidthOffset = (_orthoSize * (32 / 9))/2;
    //    float m =_flag.transform.position.y/_flag.transform.position.x;
    //    Waypoints[0].transform.position = this.transform.position + new Vector3(screenWidthOffset, _orthoSize, 0);//some position;
    //    Waypoints[1].transform.position = transform.position+ new Vector3(-screenWidthOffset,_orthoSize,0);
    //    //Waypoints[2].transform.position = transform.position+new Vector3(screenWidthOffset,_;
    //    //Waypoints[3].transform.position = //some position;
    //    //Waypoints[2].transform.position = transform.position+new Vector3(screenWidthOffset)
    //    //check up and down
    //    if (_flag.transform.position.y > transform.position.y + _orthoSize)
    //    {
    //        Debug.Log("its above me");
    //    }
    //    else if (_flag.transform.position.y < transform.position.y - _orthoSize)
    //    {
    //        Debug.Log("its below me");
    //        //_indicator.transform.position = new Vector3(_flag.transform.position.x, -_orthoSize+1);
    //    }
    //    if (_flag.transform.position.x > transform.position.x + screenWidthOffset)
    //    {
    //        Debug.Log("its on my right");
    //    }
    //    else if (_flag.transform.position.y < transform.position.y - screenWidthOffset)
    //    {
    //        Debug.Log("its on my left");
    //    }

    //}
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
            _indicatorColor.a = 1;
            _indicator.GetComponent<SpriteRenderer>().color=_indicatorColor;
            return false;
        }
    }
    bool EdgeCheck()
    {
        if (_flagCamRelativePos.x == 0)
        {

            if (_flagCamRelativePos.y > 0)
            {
                //flag is top
                _indicator.transform.position = new Vector2(0, _orthoSize);
                return true;
            }
            else if (_flagCamRelativePos.y < 0)
            {
                //_flag is below
                _indicator.transform.position = new Vector2(0, -_orthoSize);
                return true;
            }
            return true;
        }
        else if (_flagCamRelativePos.y == 0)
        {
            if (_flagCamRelativePos.x > 0)
            {
                //_flag is right
                _indicator.transform.position = new Vector2(_orthoSize,0);
                return true;
            }
            else if (_flagCamRelativePos.x < 0)
            {
                //_flag is left
                _indicator.transform.position = new Vector2( -_orthoSize,0);
                return true;
            }
            return true;
        }
        else
        {

            return false;
        }

    }
}
