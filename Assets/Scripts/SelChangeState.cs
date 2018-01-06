using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelChangeState : MonoBehaviour {

    public GameObject Sel;
    public GameObject Sel2;

	void Start () {
        Sel.SetActive(false);
        Sel2.SetActive(false);
    }
	
	void OnMouseEnter () {
        Sel.SetActive(true);
        Sel2.SetActive(false);
    }

    void OnMouseExit ()
    {
        Sel.SetActive(false);
        Sel2.SetActive(true);
    }
}
