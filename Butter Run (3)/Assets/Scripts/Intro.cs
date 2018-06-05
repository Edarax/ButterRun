using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour {
    private VideoPlayer vp;

    // Use this for initialization
    void Start () {
        vp = GetComponent<VideoPlayer>();
        StartCoroutine(Playing());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("GameScene");
        }
    }

    private IEnumerator Playing()
    {
        while (vp.isPlaying)
        {
            yield return null;
        }
        Application.LoadLevel("GameScene");
    }
}
