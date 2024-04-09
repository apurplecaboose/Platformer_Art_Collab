using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; //<--- added this for editing global volume
using UnityEngine.Rendering.Universal; //<--- added this for editing components

public class TestPostProcessing : MonoBehaviour
{

    public Volume PPVolume; 
    public Bloom _Bloom;



   // public ChromaticAberration CAeffect;


    void Start()
    {
        PPVolume = GetComponent<Volume>();



        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Bloom tmp;

            if (PPVolume.profile.TryGet<Bloom>(out tmp))
            {
                _Bloom = tmp;
                _Bloom.intensity.Override(1000);
            }

        }
        PPVolume.weight = 1;
    }
}
