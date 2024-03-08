using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderRandomRange : MonoBehaviour
{
    float _timer1, _randRefresh1, _timer2, _randRefresh2;
    Renderer _Renderer;
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
    }
    float distribution;
    float randnumb;
    void Update()
    {
        if(_timer1 >= _randRefresh1)
        {
            randnumb = Random.Range(5, 25f);
            distribution = randnumb / 10;
            _Renderer.material.SetFloat("_FlickSpeed_RandomRange", randnumb);
            _randRefresh1 = Random.Range(0.65f, 1f);
            _timer1 = 0;
        }
        else
        {
            _timer1 += distribution * Time.deltaTime;
        }

        if (_timer2 >= _randRefresh2)
        {
            _Renderer.material.SetFloat("_FlickAmount_RandomRange", Random.Range(0.65f, 0.95f));
            _randRefresh2 = Random.Range(0.5f, 01.25f);
            _timer2 = 0;
        }
        else
        {
            _timer2 += Time.deltaTime;
        }

    }
}
