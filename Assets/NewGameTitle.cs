using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameTitle : MonoBehaviour {

    public Animation cam;

    private void Awake()
    {
        Animation cam = Camera.current.GetComponent<Animation>();
    }

    private void OnMouseDown()
    {
        cam.Play("NewGame");
        Invoke("NewGame", 1f);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
