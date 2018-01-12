using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlockHover : MonoBehaviour {

    public GameObject Selection;
    public float Axisz = (float)0.64;
    public float Axisy = (float)24.1;
    public float Axisx = (float)-15.96;
    public float MarkY = (float)8.5;
    public AudioSource Dig;
    public GameObject Mark;

    void Update () {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.tag == "Dirtblock")
            {
                print("YAY! IT SHOULD WORK!");
                Selection.SetActive(true);
                Selection.transform.position = new Vector3(gameObject.transform.position.x, Axisy, gameObject.transform.position.z);
                if (Input.GetMouseButtonDown(0))
                {
                    Dig.Play();
                    Instantiate(Mark, new Vector3(Selection.transform.position.x - Axisx, MarkY, Selection.transform.position.z - Axisz), transform.rotation);
                }
            }
            if (hit.transform.tag == "Land")
            {
                print("IS IT WORKING?");
                Selection.SetActive(false);
            }
        }
    }
}
