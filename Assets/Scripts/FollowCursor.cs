using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float value;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        Vector3 finalPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(value), Mathf.Round(mousePos.z));
        transform.position = finalPos;
    }
}
