using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleUI : MonoBehaviour
{
    SlowMo Slow_Mo;
    public Image Fill1, Fill2;
    float _MaxSlowMoTime;
    private void Awake()
    {
        Slow_Mo = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMo>();
        _MaxSlowMoTime = Slow_Mo.SlowMoResourceTime; // get the max slow mo Time before it is altered
    }

    void Update()
    {
        Vector2 inputRange = new Vector2(0, _MaxSlowMoTime);
        Vector2 targetRange = new Vector2(0.025f, 0.95f);
        float dynamicfillamount = RemapValue(Slow_Mo.SlowMoResourceTime, inputRange, targetRange);
        UpdateFill(dynamicfillamount);
    }
    float RemapValue(float inputvalue, Vector2 inputrange, Vector2 targetrange)
    {
        if (inputvalue <= 0.1f) return 0;
        else if (inputvalue >= _MaxSlowMoTime - 0.1f) return 1;
        float remapnormalize0to1 = Mathf.InverseLerp(inputrange.x, inputrange.y, inputvalue);
        return Mathf.Lerp(targetrange.x, targetrange.y, remapnormalize0to1);
    }
    void UpdateFill(float amount)
    {
        Fill1.fillAmount = amount;
        Fill2.fillAmount = amount;
    }
}
;