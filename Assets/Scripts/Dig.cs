using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{

    public GameObject fortifiedObject;
    public GameObject Imp;
    public float digTime;
    public float fortifiedTime;
    public bool fortified;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fortified == false)
        {
            digTime = 10;
        }
        else

        if (digTime > 0)
        {
            Destroy(gameObject);
            Destroy(fortifiedObject);
        }
    }

    void Fortified()
    {
        Fortifying Imp = GetComponent<Fortifying>();
        if (Imp.fortifying == true)
        {

        }
    }
}