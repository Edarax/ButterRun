using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Outro : MonoBehaviour {
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
            Application.LoadLevel("Butter Run");
        }
    }

    private IEnumerator Playing()
    {
        while (vp.isPlaying)
        {
           yield return null;
        }
        Application.LoadLevel("Butter Run");
    }
}
