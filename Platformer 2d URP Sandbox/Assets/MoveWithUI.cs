using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveWithUI : MonoBehaviour
{
    [HideInInspector]public Image followTarget;
    void Update()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(followTarget.rectTransform.transform.position);
    }
}
