using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorSel : MonoBehaviour
{
    public float value;
    public float offset;
    public float grid_scale;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        Vector3 finalPos = new Vector3(Mathf.Round(mousePos.x + -offset), Mathf.Round(value), Mathf.Round(mousePos.z + offset));
        transform.position = finalPos;
        var currentPos = transform.position;
        transform.position = new Vector3(Mathf.Round(currentPos.x / grid_scale) * grid_scale,
                                     Mathf.Round(currentPos.y / grid_scale) * grid_scale,
                                     Mathf.Round(currentPos.z / grid_scale) * grid_scale);


    }
}
