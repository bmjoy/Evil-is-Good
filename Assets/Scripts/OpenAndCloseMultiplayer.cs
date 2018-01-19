using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseMultiplayer : MonoBehaviour {

    public GameObject Multiplayer;
    public GameObject Settings;
    public GameObject Credits;


    public void ClickDown()
    {
        Animation MultiplayerAnimation = Multiplayer.GetComponent<Animation>();
        if (Multiplayer.activeSelf)
        {
            MultiplayerAnimation.Play("SlideInMenu");
            Multiplayer.SetActive(false);
        }
        else
        {
            Multiplayer.SetActive(true);

            MultiplayerAnimation.Play("SlideOutMenu");
        }
    }
}
