using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Prefeb : MonoBehaviour
{
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > 4)
        {
            Destroy(gameObject);
        }
    }
}
