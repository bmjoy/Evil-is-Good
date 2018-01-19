using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour {

    public GameObject firstCanvas;
    public GameObject mapCanvas;
    public GameObject RTSCamera;
    public GameObject mapCamera;
    public bool Clicked;
    public AudioSource Click;

	// Use this for initialization
	void Start () {
        firstCanvas.SetActive(true);
        mapCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	public void OnMouseDown () {
        Click.Play();
        if (mapCanvas.activeSelf == false)
        {
            firstCanvas.SetActive(true);
            mapCanvas.SetActive(true);
            RTSCamera.SetActive(false);
            mapCamera.SetActive(true);
        }
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mapCanvas.activeSelf == true)
            {
                firstCanvas.SetActive(true);
                mapCanvas.SetActive(false);
                RTSCamera.SetActive(true);
                mapCamera.SetActive(false);
            }
        }
    }
}
