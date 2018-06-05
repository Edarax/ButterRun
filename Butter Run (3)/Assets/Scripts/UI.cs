using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    public GameObject controls;
    public GameObject credits;
    public GameObject menu;

    // Use this for initialization
    void Start () {
        credits.SetActive(false);
        controls.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("Intro");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Controls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
        controls.SetActive(false);
        menu.SetActive(true);
    }

}
