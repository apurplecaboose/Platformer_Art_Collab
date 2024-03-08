using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    float currentRefreshRate;
    int currentResolutionIndex=0;

    private void Start()
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
    
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
