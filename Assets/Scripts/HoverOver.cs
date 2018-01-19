using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    public GameObject selection;
    public AudioSource dig;           // Sound

    [Header("Positions")]
    public float axisX = -17.13f;     // X Offset
    public float axisY = 24.1f;       // Y Value
    public float axisZ = 15.92f;      // Z Offset
    public float markY = 4.5f;        // For the marker

    [Header("Marker Detection")]
    public GameObject marker;         // Marker
    public LayerMask markerLayer;    // Marker layer
    public float markerRadius;   // Radius of sphere
    public Vector3 markerPosition; // Center of sphere

    private Transform t;             // This gameobject's transform
    private GameObject markerObject;  // Marker instantiated clone

    void Awake()
    {
        t = gameObject.transform;
    }

    // Upon entering the dirtblock, gold, or Gems object which this script is attached to
    private void OnMouseEnter()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Hit something?
        if (Physics.Raycast(ray, out hit, 300))
        {
            Debug.Log("You hit something!");
            selection.SetActive(true);

            // Set to current position with offset
            selection.transform.position = new Vector3(t.position.x + axisX, axisY, t.position.z + axisZ);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.CheckSphere(markerPosition, markerRadius, markerLayer))
                {
                    Debug.Log("Clearing marker...");
                    dig.Play();
                    markerObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Placing marker...");
                    dig.Play();

                    // Reposition marker
                    markerObject.SetActive(true);
                    markerObject = Instantiate(marker, new Vector3(t.position.x, t.position.y + markY, t.position.z), t.rotation);
                }
            }
            // Left click?
            if (Input.GetMouseButton(0))
            {
                // Hit marker?
                if (Physics.CheckSphere(markerPosition, markerRadius, markerLayer))
                {
                    Debug.Log("Clearing marker...");
                    dig.Play();
                    markerObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Placing marker...");
                    dig.Play();

                    // Reposition marker
                    markerObject = Instantiate(marker, new Vector3(t.position.x, t.position.y + markY, t.position.z), t.rotation);
                }
            }
        }

        // Hit bedrock?
        if (hit.collider.CompareTag("Bedrock"))
        {
            selection.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        selection.SetActive(false);
    }
}