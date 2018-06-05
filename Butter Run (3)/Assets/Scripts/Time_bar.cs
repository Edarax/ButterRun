using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_bar : MonoBehaviour {

    private static float TIME = 70f;

    private float currentTime;

    private static float y;

    private static float END = 14.4f;

    private static float intervalSize;

	// Use this for initialization
	void Start () {
        y = transform.localPosition.y;
        intervalSize = Mathf.Abs(transform.localPosition.x) + END;
        currentTime = TIME;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            Application.LoadLevel("Outro1");
        }
        float x = END - (currentTime / TIME) * intervalSize;
        transform.localPosition = new Vector3(x, y, -1);
	}
}
