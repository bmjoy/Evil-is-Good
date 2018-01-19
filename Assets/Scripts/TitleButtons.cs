using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour {

    public AudioSource click;
    public Animation anim;
    public Camera cam;

    private void Awake()
    {
        Animation anim = gameObject.GetComponent<Animation>();
    }

    private void OnMouseOver()
    { 
        anim.Play("Hover");
    }

    private void OnMouseExit()
    {
        anim.Play("HoverExit");
    }

    private void OnMouseDown()
    {
        click.Play();
        anim.Play("Click");
    }
}
