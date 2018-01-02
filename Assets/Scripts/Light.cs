using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
        Vector3 finalPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), Mathf.Round(mousePos.z));
        transform.position = finalPos;
    }
}
