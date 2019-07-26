using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerballbehavior : MonoBehaviour
{
    //members
    float maxSpeed = 6.40f;
    float moveForce = 256.0f;
    float jumpForce = 6144.0f;
    float restartTimer = 3f;
    int maxJumps = 2;
    int jumpCounter = 0;
    int totalBoops = 0;
    int lives = 3;
    Rigidbody2D rigidbody = null;
    TextMesh boopText = null;
    TextMesh livesText = null;
    public AudioClip jumpSound;
    public AudioClip dieSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boopText = GameObject.FindGameObjectWithTag("boopstext").GetComponent<TextMesh>();
        livesText = GameObject.FindGameObjectWithTag("livestext").GetComponent<TextMesh>();
        transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //update boops text
        boopText.text = "Bops: " + totalBoops;
        livesText.text = "Lives: " + lives;

        float h = Input.GetAxisRaw("Horizontal");

        //face player
        if (h < 0) transform.localScale = new Vector3(2, 2, 1);
        if (h > 0) transform.localScale = new Vector3(-2, 2, 1);

        if (h * rigidbody.velocity.x < maxSpeed)
            // ... add a force to the player.
            rigidbody.AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCounter > 0)
            {
                SoundManager.instance.playSingle(jumpSound);
                rigidbody.AddForce(Vector2.up * jumpForce);
                jumpCounter = jumpCounter - 1;
            }
        }
        transform.eulerAngles = Vector3.zero;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "ground") 
        {
            jumpCounter = maxJumps;
        }
        if (collision.collider.tag == "enemybody")
        {

            boop();
            lives--;
            SoundManager.instance.playSingle(dieSound);
            reset();
            if (lives < 1)
            {
                //Game Over Debug.Log("destroyed");
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Respawn").GetComponent<Playerspawn>().playerDied();

                GameObject.FindGameObjectWithTag("GameOver").transform.position = new Vector3(-5.5f, 1.5f, -9f);

                //pause
                restartTimer = restartTimer - Time.deltaTime;
                if (restartTimer < 0)
                {

                    GameObject.FindGameObjectWithTag("GameOver").transform.position = new Vector3(-5.5f, 1.5f, 21f);

                    GameObject.FindGameObjectWithTag("Respawn").GetComponent<Playerspawn>().spawn();
                    GameObject.FindGameObjectWithTag("enemyspawner").GetComponent<EnemySpawnerScript>().reset();
                    restartTimer = 3f;
                }
            }
        }
        if (collision.collider.tag == "enemyhead")
        {
            boop();
            totalBoops = totalBoops + 1;
            //collision.collider.gameObject.GetComponent<Badguybehavior>().kill();
        }
    }

    void boop()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * (jumpForce / 4f));
    }

    void reset()
    {
        rigidbody.velocity = Vector2.zero;
        transform.position = new Vector2(-1.25f, 0f);
    }

    public IEnumerator PauseGame(float pauseTime)
    {
        Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with my pause");
    }
}
