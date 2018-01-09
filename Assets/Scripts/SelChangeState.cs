using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelChangeState : MonoBehaviour {

    public GameObject Sel;
    public GameObject Sel2;
    public GoldRoom Bool;
    public GameObject Dirt;


    void OnMouseExit ()
    {
        Sel.SetActive(false);
        Bool = GetComponent<GoldRoom>();
        if (Bool.buttondown == true)
        {
            Sel2.SetActive(true);
        }
    }
}
