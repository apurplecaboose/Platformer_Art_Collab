using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderRandomRange : MonoBehaviour
{
    float _timer, _randRefresh;
    Renderer _Renderer;
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(_timer >= _randRefresh)
        {
            _Renderer.material.SetFloat("_FlickSpeed_RandomRange", Random.Range(5, 25f));
            _Renderer.material.SetFloat("_FlickAmount_RandomRange", Random.Range(0.65f, 0.95f));
            _randRefresh = Random.Range(0.5f, 1f);
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }

    }
}
