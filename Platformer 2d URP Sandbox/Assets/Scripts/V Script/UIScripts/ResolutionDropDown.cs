using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropDown : MonoBehaviour
{
    [SerializeField]  TMP_Dropdown resolutionDropdown;
     Resolution[] resolutions;
     List<Resolution> filteredResolutions;

    float currentRefreshRate;
    int currentResolutionIndex=0;

    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        List<string>options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width==Screen.currentResolution.width&&
                resolutions[i].height==Screen.currentResolution.height)
            { 
            currentRefreshRate = i;
            }
          //  resolutionDropdown.AddOptions(option)
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
