using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionDropDownScript : MonoBehaviour
{
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
    }
    public void ScreenResFunction(int val)
    {
        if (val == 0)
        {
            Screen.SetResolution(640, 360, true);
            
        }
        if (val == 1)
        {
            Screen.SetResolution(854, 480, true); 
        }
        if (val == 2)
        {
            Screen.SetResolution(1280, 720, true); 
        }
        if (val == 3)
        {
            Screen.SetResolution(1920, 1080, true); 
        }
        if (val == 4)
        {
            Screen.SetResolution(2560, 1440, true); 
        }
        if (val == 5)
        {
            Screen.SetResolution(3840, 2160, true); 
        }

        
     
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
