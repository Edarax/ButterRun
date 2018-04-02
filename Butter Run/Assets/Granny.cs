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
    /* initial posotion of object */
    private Vector2 start;

    // Use this for initialization
    void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
        isCrawling = false;
        isAttacking = false;
        isStunned = false;
        isGrounded = true;
        start = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate()
    {
        if (!isStunned)
        {
            float horizontal = 4 * Input.GetAxis("Horizontal");
            body.velocity = new Vector2(horizontal, body.velocity.y);

            if (!isCrawling)
            {
                if (isGrounded)
                {
                    if (Input.GetButton("Vertical") && !isAttacking)
                    {
                        isCrawling = true;
                        switchStance();
                    }

                    else if (Input.GetButtonDown("Jump"))
                    {
                        body.AddForce(transform.up * 6, ForceMode2D.Impulse);
                        isGrounded = false;
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
                switchStance();
            }    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.Equals("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isAttacking && collision.gameObject.GetComponent<Obstacle>().isDestructable())
        {
            Destroy(collision.gameObject);
            //collision.gameObject.transform.localPosition = new Vector2(-50, 0);
        }
        else
        {
            float offset = 0.6f;
            float stunTime = collision.gameObject.GetComponent<Obstacle>().getStunTime();
            StartCoroutine(stun(stunTime, collision.gameObject.transform.localPosition.x - offset));

        }
    }

    /* when crounching it gets the center of the box colider where it suppose to be */
    private void switchStance()
    {
        float delta = (transform.localScale.x - transform.localScale.y) / 2;
        transform.localScale = new Vector2(transform.localScale.y, transform.localScale.x);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + delta);
    }

    /* durning attack the box collider grows bigger and objcet is move to right at the end it return to normal */
    private IEnumerator attack()
    {
        float delta = 0.3f * transform.localScale.x;
        isAttacking = true;
        transform.localPosition = new Vector2(transform.localPosition.x + delta/2, transform.localPosition.y);
        transform.localScale = new Vector2(transform.localScale.x + delta, transform.localScale.y);
        
        yield return new WaitForSeconds(0.5f);

        transform.localPosition = new Vector2(transform.localPosition.x - delta / 2, transform.localPosition.y);
        transform.localScale = new Vector2(transform.localScale.x - delta, transform.localScale.y);
        isAttacking = false;
    }

    /* if object steps in obstacle it is stunned for a stunTime in sec and move slightly before the obstacle to stunLocation*/
    private IEnumerator stun(float stunTime, float stunLocation)
    {
        isStunned = true;
        body.velocity = new Vector2(0, 0);
        transform.localPosition = new Vector2(stunLocation, start.y);

        yield return new WaitForSeconds(stunTime);

        isStunned = false;
    }
}
