using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_Game_UI : MonoBehaviour {

    private bool paused = false;

    public GameObject gameUI;

	// Use this for initialization
	void Start () {
        gameUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        gameUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    private void Pause()
    {
        gameUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void BackToMenu()
    {
        Application.LoadLevel("Butter run");
    }
}
