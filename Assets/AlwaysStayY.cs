using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysStayY : MonoBehaviour {

    public float always = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 20)
        {
            Transform();
        }
    }

    private void Transform()
    {
        Vector3 temp = new Vector3(transform.position.x, 0, transform.position.y);
        gameObject.transform.position += temp;
    }
}
