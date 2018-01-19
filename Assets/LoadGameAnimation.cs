using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameAnimation : MonoBehaviour {

    public AudioSource audio;
    public Animation cam;

    private void Awake()
    {
        Animation cam = Camera.current.GetComponent<Animation>();
    }

    private void OnMouseOver()
    {
        cam.Play("Hover");
    }

    private void OnMouseExit()
    {
        cam.Play("HoverExit");
    }

    private void OnMouseDown()
    {
        audio.Play();
        cam.Play("HoverClick");
    }
}

