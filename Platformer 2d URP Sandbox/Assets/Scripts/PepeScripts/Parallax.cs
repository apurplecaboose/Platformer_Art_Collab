using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    float _length, _startPos;
    public GameObject Cam;
    public float ParallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = Cam.transform.position.x * (1 - ParallaxEffect);
        float dist = Cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if(temp > _startPos + _length)
        {
            _startPos += _length;
        }
        else if (temp < _startPos - _length)
        {
            _startPos -= _length;
        }
    }
}
