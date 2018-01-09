using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursorSel : MonoBehaviour
{
    public float offset;
    public GameObject DirtBlock;
    public GameObject Selection;
    public GameObject Button;

    void OnMouseEnter()
    {
        Selection.transform.position = new Vector3(DirtBlock.transform.position.x + -offset, DirtBlock.transform.position.y, DirtBlock.transform.position.z + offset);
    }
    void OnMouseExit()
    {
        gameObject.SetActive(false);
        GoldRoom Button = GetComponent<GoldRoom>();
        if (Button.buttondown == true)
        {

        }
    }
}
