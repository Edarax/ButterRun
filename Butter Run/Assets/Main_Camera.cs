using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera : MonoBehaviour {
    public GameObject granny;
    private static float OFFSET;
	// Use this for initialization
	void Start () {
        OFFSET = transform.position.x - granny.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(granny.transform.localPosition.x + OFFSET, transform.localPosition.y, transform.localPosition.z);
    }
}
