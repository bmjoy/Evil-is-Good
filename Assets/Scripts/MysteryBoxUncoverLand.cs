using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxUncoverLand : MonoBehaviour {

    private Vector3 C;
    public GameObject FoWPlane;
    public GameObject MysteryBox;
    public Camera Cam;
    Vector2 mousePos = new Vector2();


    // Update is called once per frame
    void OnMouseEnter () {
        C = Cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Cam.nearClipPlane));
        if (Input.GetMouseButtonDown(0))
        {
            FoWPlane.SetActive(false);
            Animation();
        }
	}
    public void Animation()
    {
        Animation MysteryboxAnimation = MysteryBox.GetComponent<Animation>();
        MysteryboxAnimation.Play("Shrink");
    }
}
