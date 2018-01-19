using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlockHover : MonoBehaviour {


    // Setting up
    public GameObject Selection;
    public float Axisz = (float)15.92; // Offset
    public float Axisy = (float)24.1; // Y Value
    public float Axisx = (float)-17.13; // Offset
    public float MarkY = (float)4.5; // For the Marker
    public AudioSource Dig; // Sound
    public GameObject Mark; // Marker
    public Vector3 spawnPos; // CheckSphere() position
    public float radius; // CheckSphere() radius
    private GameObject MarkObject; // Marker Instantiated Clone
    public GameObject Selection1;

    private void OnMouseOver() // Upon entering the dirtblock, gold, or Gems object which this script is attached to
    {
        RaycastHit hit = new RaycastHit(); // Raycasting!!
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // SPTR = ScreenPointToRay
        if (Physics.Raycast(ray, out hit, 1000)) // Set the ray to 'ray' and it's length to 1000 units.
        {
            print("YAY! IT SHOULD WORK!"); // debugging tool!
            Selection.SetActive(true); // Turning the SelectionBox on
            Selection.transform.position = new Vector3(gameObject.transform.position.x + Axisx, Axisy, gameObject.transform.position.z + Axisz); // Set selection box to current objects position and add some
            if (Input.GetMouseButton(0)) // When my mouse clicks
            {
                if (Physics.CheckSphere(spawnPos, radius, 20)) // Check if there is already a marker above the object, Layer 20 is the Marker Layer
                {
                    print("Destroying Mark"); // more debugging
                    Dig.Play(); // Play a dig sound
                    Destroy(Mark); // DESTROY IT
                }
                else
                {
                    print("Adding Mark"); // another debugging thingy
                    Dig.Play(); // Play a dig sound
                    MarkObject = Instantiate(Mark, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + MarkY, gameObject.transform.position.z), transform.rotation); // add the Marker to the world.
                }
            }
        }
        if (hit.transform.tag == "Land")
        {
            Selection1.SetActive(true);
        }
        /* if (hit.transform.tag == "Land")
        {
        print("IS IT WORKING?");
        Selection.SetActive(false);
        }
        GameObject[] childObjects = Selection.GetComponentsInChildren<GameObject>();
        */
        // Up above used to play a previous role. Now I am just keeping it just incase I change my mind on some feature.

            if (hit.transform.tag == "Bedrock") // If it hits bedrock
        {
            Selection.SetActive(false);
            // Like I said, just incase I want to change it. 
        }


    }

    private void OnMouseExit()
    {
        Selection.SetActive(false);
    }
}
