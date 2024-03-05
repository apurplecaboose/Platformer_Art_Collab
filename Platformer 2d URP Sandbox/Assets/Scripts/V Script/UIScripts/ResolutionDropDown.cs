using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionDropDown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDroppdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    float currentRefreshRate;
    int currentResolutionIndex=0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDroppdown.ClearOptions();
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
