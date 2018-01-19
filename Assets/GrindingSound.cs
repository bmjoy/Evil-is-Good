using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindingSound : MonoBehaviour {

    public AudioSource grinding;

	void OnMouseEnter () {
        grinding.Play();
    }
}
