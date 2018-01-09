using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldRoom : MonoBehaviour {

    public GameObject MousePosition;
    public GameObject Selection;
    public AudioSource notenoughgold;
    public GameObject goldcount;
    public Text goldcounter;
    private float floatCount;
    public bool buttondown;
	
    // Use this for initialization
	void Start () {
        MousePosition.SetActive(false);
        Selection.SetActive(false);
	}
	
    public void Click ()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            goldcounter = GetComponent<Text>();
            floatCount = int.Parse(goldcounter.text);
            buttondown = true;
            if (floatCount > 50)
            {
               notenoughgold.Play();
            }
            else if (floatCount <= 50)
            {

            }
            
        }
    }
}
