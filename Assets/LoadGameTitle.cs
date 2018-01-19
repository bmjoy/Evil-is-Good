using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameTitle : MonoBehaviour {

    public Animation cam;

    private void Awake()
    {
        Animation cam = Camera.current.GetComponent<Animation>();
    }

    private void OnMouseDown()
    {
        cam.Play("LoadGame");
    }
}
