using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour {

	// Use this for initialization
	public void Play () {
        SceneManager.LoadScene("LevelSelect");
    }
	public void Multiplayer()
    {

    }
}
