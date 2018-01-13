using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlockHover : MonoBehaviour {

    public GameObject Selection;
    public float Axisz = (float)0.64;
    public float Axisy = (float)24.1;
    public float Axisx = (float)-15.96;
    public float MarkY = (float)4.5;
    public AudioSource Dig;
    public GameObject Mark;
    public Vector3 spawnPos;
    public float radius;

    private void OnMouseEnter()
    {
        GameObject[] Sel;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            print("YAY! IT SHOULD WORK!");
            Selection.SetActive(true);
            Selection.transform.position = new Vector3(gameObject.transform.position.x + Axisx, Axisy, gameObject.transform.position.z - Axisz);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.CheckSphere(spawnPos, radius))
                {
                    Destroy(gameObject);
                }
                else
                {
                    Dig.Play();
                    Instantiate(Mark, new Vector3(gameObject.transform.position.x - Axisx, gameObject.transform.position.y + MarkY, gameObject.transform.position.z - Axisz), transform.rotation);
                }
            }
        }
        if (hit.transform.tag == "Land")
        {
        print("IS IT WORKING?");
        Selection.SetActive(false);
        }
        GameObject[] childObjects = Selection.transform.GetComponentsInChildren<GameObject>();

        if (hit.transform.tag == "Bedrock")
        {
            print("BEDROCK HAS BEEN HIT LOL");
            foreach (GameObject j in childObjects)
            {
                j.GetComponent<Renderer>().material.color = Color.red;
            }
                
        }
        else
        {
            foreach (GameObject j in childObjects)
            {
                j.GetComponent<Renderer>().material.color = Color.green;
            }
        }


    }

    private void OnMouseExit()
    {
        Selection.SetActive(false);
    }
}
