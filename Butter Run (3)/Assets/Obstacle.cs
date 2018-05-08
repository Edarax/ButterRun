using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    /* enum of different types of obstacles*/
    enum ObstacleType {destructable, ground, crawling };

    /*this obstacles type*/
    private ObstacleType type;

    /* how long will player be stuned if obstacle is hit in sec */
    private float stunTime;

    private Animator anim;

    private float offset;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        if (this.name.StartsWith("Obstacle_crawling"))
        {
            type = ObstacleType.crawling;
            stunTime = 2f;
            offset = 4f;
        }
        else if (this.name.StartsWith("Obstacle_ground"))
        {
            type = ObstacleType.ground;
            stunTime = 3f;
            offset = 0f;
        }
        else if (this.name.StartsWith("Obstacle_destructable"))
        {
            type = ObstacleType.destructable;
            stunTime = 4f;
            offset = 4f;
        }
        /*in case of error*/
        else
        {
            type = ObstacleType.destructable;
            stunTime = 0f;
            offset = 0f;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /* returns boolean if obstacle can be destroyed */
    public bool isDestructable()
    {
        if (type.Equals(ObstacleType.destructable))
        {
            return true;
        }
        return false;
    }

    public bool isCrawlable()
    {
        if (type.Equals(ObstacleType.crawling))
        {
            return true;
        }
        return false;
    }

    public bool isJumpable()
    {
        if (type.Equals(ObstacleType.ground))
        {
            return true;
        }
        return false;
    }

    /* returns time spend in sec if player wont avoid this obstacle */
    public float getStunTime()
    {
        return stunTime;
    }

    public float getOffset()
    {
        return offset;
    }

    public void obstacleFail()
    {
        if (anim != null) {
            if (!name.StartsWith("Obstacle_destructable_di"))
            {
                anim.SetTrigger("Fail");
            }
        }
    }

    public void obstacleSucces()
    {
        if (anim != null)
        {
            if (!name.StartsWith("Obstacle_destructable_di"))
            { 
                anim.SetTrigger("Succes");
            }
            else
            {
                transform.localPosition = new Vector2(0, 50);
            }
            
            if (type.Equals(ObstacleType.destructable))
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 50);
            }
        }
        else
        {
            transform.localPosition = new Vector2(0, 50);
        }
    }
}
