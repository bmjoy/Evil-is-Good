using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possess : MonoBehaviour {


    public GameObject FPSCamera;
    public GameObject RTSCamera;
    public GameObject Test;
    public GameObject Profile;
    public AudioSource POSSESS;

	// Update is called once per frame
	public void OnMouseClick () {
        POSSESS.Play();
        FPSCamera.SetActive(true);
        RTSCamera.SetActive(false);
        Test.SetActive(false);
        Profile.SetActive(true);
    }
}
