using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granny : MonoBehaviour {
    /* if object is crawiling*/
    private bool isCrawling;
    /* if object is attackin */
    private bool isAttacking;
    /* if object is stunned */
    private bool isStunned;
    /* if object is on ground */
    private bool isGrounded;
    /* body of the object */
    private Rigidbody2D body;
    /*animator acces*/
    private Animator anim;

    private static int MOVMENTSPEED = 20;
    private static int JUMPHEIGHT = 6;

    // Use this for initialization
    void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
        isCrawling = false;
        isAttacking = false;
        isStunned = false;
        isGrounded = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!isStunned)
        {
            float horizontal = MOVMENTSPEED * Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontal, body.velocity.y);

            if (!isCrawling)
            {
                if (isGrounded)
                {
                    if (Input.GetButton("Vertical") && !isAttacking)
                    {
                        isCrawling = true;
                        anim.SetBool("isCrawling", true);
                    }

                    else if (Input.GetButtonDown("Jump"))
                    {
                        body.AddForce(transform.up * JUMPHEIGHT, ForceMode2D.Impulse);
                        isGrounded = false;
                        anim.SetBool("isGrounded", false);
                    }


                }

                if (Input.GetButtonDown("Fire1") && !isAttacking)
                {
                    StartCoroutine(attack());
                }
            }

            else if (Input.GetButtonUp("Vertical"))
            {
                isCrawling = false;
                anim.SetBool("isCrawling", false);
            }
        }
    }

    private void FixedUpdate()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.Equals("Floor"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.gameObject.GetComponent<Obstacle>().isDestructable())
        {
            collision.gameObject.GetComponent<Obstacle>().obstacleSucces();
        }
        else if (isCrawling && collision.gameObject.GetComponent<Obstacle>().isCrawlable())
        {

        }
        else
        {
            float offset = collision.gameObject.GetComponent<Obstacle>().getOffset();
            float stunTime = collision.gameObject.GetComponent<Obstacle>().getStunTime();
            if (collision.gameObject.GetComponent<Obstacle>().isJumpable())
            {
                anim.SetTrigger("Fall");
            }

            collision.gameObject.GetComponent<Obstacle>().obstacleFail();
            StartCoroutine(stun(stunTime, collision.gameObject.transform.localPosition.x - offset));

        }
    }

    /* durning attack the box collider grows bigger and objcet is move to right at the end it return to normal */
    private IEnumerator attack()
    {
        
        isAttacking = true;
        anim.SetBool("isAttacking", true);

        /*
        float delta = 0.3f * transform.localScale.x;
        transform.localPosition = new Vector2(transform.localPosition.x + delta/2, transform.localPosition.y);
        transform.localScale = new Vector2(transform.localScale.x + delta, transform.localScale.y);
        */
        yield return new WaitForSeconds(0.5f);

        /*
        transform.localPosition = new Vector2(transform.localPosition.x - delta / 2, transform.localPosition.y);
        transform.localScale = new Vector2(transform.localScale.x - delta, transform.localScale.y);
        */

        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    /* if object steps in obstacle it is stunned for a stunTime in sec and move slightly before the obstacle to stunLocation*/
    private IEnumerator stun(float stunTime, float stunLocation)
    {
        isStunned = true;
        body.velocity = new Vector2(0, 0);
        transform.localPosition = new Vector2(stunLocation, transform.localPosition.y);

        yield return new WaitForSeconds(stunTime);

        isStunned = false;
    }
}
