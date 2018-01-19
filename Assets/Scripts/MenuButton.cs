using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public GameObject Panel;
    public Animation SlideIn;
    public Animation SlideOut;
    public float Timer;
	
	// Update is called once per frame
	public void ClickDown () {
        Animation PanelAnimation = Panel.GetComponent<Animation>();
        Timer -= Time.deltaTime;
        if (Panel.activeSelf)
        {
            PanelAnimation.Play("SlideInMenu");
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);

            PanelAnimation.Play("SlideOutMenu");
        }
	}
}
