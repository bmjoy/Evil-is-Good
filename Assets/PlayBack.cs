using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBack : MonoBehaviour {

    public Animation cam;

    private void Awake()
    {
        Animation anim = Camera.current.GetComponent<Animation>();
    }

    // Update is called once per frame
    void OnMouseDown () {
        cam.Play("CamPlayBack");
    }
}
