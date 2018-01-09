using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimator : MonoBehaviour {

    

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Animator>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
