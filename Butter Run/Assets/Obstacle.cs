using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    /* if object is destructable */
    private bool destructable;
    /* how long will player be stuned if obstacle is hit in sec */
    private float stunTime;

	// Use this for initialization
	void Start () {
		if (this.name.StartsWith("Obstacle_Annoying_Neighbor"))
        {
            destructable = true;
            stunTime = 3f;
        }
        else
        {
            destructable = false;
            stunTime = 2f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /* return if object is destructable by player */
    public bool isDestructable()
    {
        return destructable;
    }

    /* returns time spend in sec if player wont avoid this obstacle */
    public float getStunTime()
    {
        return stunTime;
    }
}
