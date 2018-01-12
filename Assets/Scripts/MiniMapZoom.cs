using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapZoom : MonoBehaviour {
    public GameObject MiniMap;

    public void OnPlus ()
    {
        if (MiniMap.GetComponent<Camera>().fieldOfView < 120)
        {
            MiniMap.GetComponent<Camera>().fieldOfView = MiniMap.GetComponent<Camera>().fieldOfView + 20;
        }
    }
    public void OnMinus ()
    {
        if (MiniMap.GetComponent<Camera>().fieldOfView > 20)
        {
            MiniMap.GetComponent<Camera>().fieldOfView = MiniMap.GetComponent<Camera>().fieldOfView - 20;
        }

    }
}
