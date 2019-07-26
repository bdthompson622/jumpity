using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badguybehavior : MonoBehaviour
{
    public bool moveRight = true;
    public bool move = false;
    public bool dead = false;
    float speed = 128f;
    float maxSpeed = 0.8f;
    float jumpTimer;
    float jumpForce = 2048f;
    Rigidbody2D body = null;
    public AudioClip boing;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = (Random.value > .5f);
        if (moveRight) transform.localScale = new Vector3(-1f, 1f, 1);
        jumpTimer = Random.Range(1f, 3f);

        body = GetComponent<Rigidbody2D>();
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            //jump logic
            jumpTimer = jumpTimer - Time.deltaTime;
            if (jumpTimer < 0)
            {
                SoundManager.instance.playSingle(boing);
                body.AddForce(Vector2.up * jumpForce);
                jumpTimer = Random.Range(1f, 3f);
            }
            //push right
            if (moveRight)  body.AddForce(Vector2.right * speed);
            if (!moveRight) body.AddForce(Vector2.left * speed);
            if (Mathf.Abs(body.velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
            transform.eulerAngles = Vector3.zero;
        }
    }

    void OnBecameInvisible()
    {
        //respawn();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "ground") && dead == false) move = true;
    }

    public void respawn()
    {
        float xloc = Random.Range(-3f, 3f);
        transform.position = (new Vector2(xloc, 5));
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        move = false;
        moveRight = (Random.value > .5f);
    }

    public void kill()
    {
        Destroy(gameObject, .1f);
    }
}
