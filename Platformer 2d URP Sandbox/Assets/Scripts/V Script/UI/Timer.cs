using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    public float timer;
    //supposed to be a timer to calculate player progress using unscaled deltatime.
    private void Start()
    {
      
    }
    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        TMP.text = timer + "";
    }

}
