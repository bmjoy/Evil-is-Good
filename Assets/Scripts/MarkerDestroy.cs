using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerDestroy : MonoBehaviour {
	
	// Update is called once per frame
	void OnMouseDown () {
        Destroy(gameObject);
	}
}
