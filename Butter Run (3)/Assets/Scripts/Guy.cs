using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour {
    private Rigidbody2D body;

    private static float MOVEMENTSPEED = 6;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        body.velocity = new Vector2(MOVEMENTSPEED, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.StartsWith("Guy"))
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 30, transform.localPosition.y);
        }
    }
}
