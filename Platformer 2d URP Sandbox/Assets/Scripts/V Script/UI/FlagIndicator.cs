using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagIndicator : MonoBehaviour
{
    /// <summary>
    /// Attach as a child of the flag gameobject. (the gameobject with this script on it must have a sprite renderer with the indicator on it)
    /// </summary>

    Transform _flagTransform;
    Camera _cam;
    SpriteRenderer _sr;

    public Gradient DistanceColorGradient;
    private void Awake()
    {
        _cam = Camera.main;
        _sr = this.GetComponent<SpriteRenderer>();

        _flagTransform = transform.parent;
    }
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, _flagTransform.position);
        MoveIndicator();
        if (distance < 1)
        {
            this.transform.localScale = Vector3.zero; //bad code alert lel, sets transform to zero when indicator is offscreen to make it invisible
        }
        else
        {
            //no need to run these functions if distance check returns too close 
            RotateIndicator();
            DistanceBasedModifiers(distance);
        }
    }

    void MoveIndicator()
    {
        Vector3 topR_screenWorldSpace = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 botL_screenWorldSpace = _cam.ScreenToWorldPoint(Vector3.zero);
        float screenedge_offset = 0.75f;
        Vector2 x_Clamp = new Vector2(botL_screenWorldSpace.x + screenedge_offset, topR_screenWorldSpace.x - screenedge_offset);
        Vector2 y_Clamp = new Vector2(botL_screenWorldSpace.y + screenedge_offset, topR_screenWorldSpace.y - screenedge_offset);

        this.transform.position = new Vector3(Mathf.Clamp(_flagTransform.position.x, x_Clamp.x, x_Clamp.y), Mathf.Clamp(_flagTransform.position.y, y_Clamp.x, y_Clamp.y), 0);
    }
    void RotateIndicator()
    {
        float angleoffset = -30;
        Vector2 direction = _flagTransform.position - _cam.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(0,0, angleoffset);
    }
    void DistanceBasedModifiers(float distance)
    {
        float minimumIndicatorScale = 0.35f;
        float startgrowingrange = 10f;
        float maxSizeRange = 3.5f; //the indicator will be at scale 1 after this point
        float remapedScalebyDistance = Mathf.InverseLerp(startgrowingrange, maxSizeRange, distance);//remap the scale modifier using inverse lerp
        _sr.color = DistanceColorGradient.Evaluate(remapedScalebyDistance); // write color before clamp dummy
        remapedScalebyDistance = Mathf.Clamp(remapedScalebyDistance, minimumIndicatorScale, 1f);// clamp the minimum size of the indicator
        this.transform.localScale = new Vector3(remapedScalebyDistance, remapedScalebyDistance, 0);
    }
}
