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
    /**/
    private bool facingRight = true;

    private static int MOVMENTSPEED = 5;
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
        if (!isStunned && Time.timeScale > 0f)
        {           
            float horizontal = MOVMENTSPEED * Input.GetAxis("Horizontal");
            if (horizontal > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && facingRight)
            {
                Flip();
            }
                
            body.velocity = new Vector2(horizontal, body.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(horizontal));

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
                    StartCoroutine(Attack());
                }
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            isCrawling = false;
            anim.SetBool("isCrawling", false);
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
        if (collision.collider.gameObject.name.StartsWith("Guy") && isAttacking)
        {
            Application.LoadLevel("Outro2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle script = collision.gameObject.GetComponent<Obstacle>();

        /*when attacking destroying destructable obstacles*/
        if (isAttacking && script.isDestructable())
        {
            script.obstacleSucces();
        }
        /* when crawling ignoring crawlable obastalces */
        else if (isCrawling && script.isCrawlable())
        {

        }
        /* in case of falling obstacle */
        else
        {
            float offset = script.getOffset();
            if (!facingRight)
            {
                offset = -offset;
            }
            float stunTime = script.getStunTime();

            if (script.isJumpable())
            {
                anim.SetTrigger("Fall");
            }

            script.obstacleFail();
            string name = collision.gameObject.name;
            if (name.StartsWith("Obstacle_crawling_sale") || name.StartsWith("Obstacle_destructable"))
            {
                anim.SetTrigger("Intrest");
            }
            StartCoroutine(Stun(stunTime, collision.gameObject.transform.localPosition.x - offset));

        }
    }

    /* durning attack the box collider grows bigger and objcet is move to right at the end it return to normal */
    private IEnumerator Attack()
    { 
        float attackOffset = 0.5f;
        isAttacking = true;
        anim.SetBool("isAttacking", true);

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(box.size.x + attackOffset, box.size.y);

        yield return new WaitForSeconds(0.5f);

        box.size = new Vector2(box.size.x - attackOffset, box.size.y);

        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    /* if object steps in obstacle it is stunned for a stunTime in sec and move slightly before the obstacle to stunLocation*/
    private IEnumerator Stun(float stunTime, float stunLocation)
    {
        isStunned = true;
        anim.SetBool("isStuned", true);
        body.velocity = new Vector2(0, 0); 
        anim.SetFloat("Speed", 0);
        transform.localPosition = new Vector3(stunLocation, transform.localPosition.y, transform.localPosition.z);

        yield return new WaitForSeconds(stunTime);

        isStunned = false;
        anim.SetBool("isStuned", false);
    }

    /* makes granny look the other way */
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
