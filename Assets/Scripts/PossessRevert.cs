using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessRevert : MonoBehaviour {

    public GameObject FPSCamera;
    public GameObject RTSCamera;
    public GameObject Test;
    public GameObject Profile;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            FPSCamera.SetActive(false);
            RTSCamera.SetActive(true);
            Test.SetActive(true);
            Profile.SetActive(false);
        }
	}
}
