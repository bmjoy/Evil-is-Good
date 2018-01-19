using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerBack : MonoBehaviour
{

    public Animation cam;

    private void Awake()
    {
        Animation cam = Camera.current.GetComponent<Animation>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        cam.Play("CamMultiplayerBack");
    }
}
