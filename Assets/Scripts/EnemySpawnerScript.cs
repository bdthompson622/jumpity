using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy = null;
    TextMesh spawntext = null;
    float spawnTime = 5f;
    float spawnTimer;
    float incrimentDelta = 0.1f;
    float incrimentTimer;
    float incrimentTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTime;
        incrimentTimer = incrimentTime;
        spawntext = GameObject.FindGameObjectWithTag("spawntext").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        //debug
        //Debug.Log(spawnTime);
        //update text
        spawntext.text = "Spawn Timer:" + System.Math.Round(spawnTimer, 1) + "\nSpawn Interval:" + System.Math.Round(spawnTime, 1) + "\nIncrement Timer:" + System.Math.Round(incrimentTimer, 3);

        //Update timers
        spawnTimer = spawnTimer - Time.deltaTime;
        incrimentTimer = incrimentTimer - Time.deltaTime;

        //time to spawn new enemy
        if (spawnTimer < 0)
        {
            //spawn new enemy
            GameObject enemyClone = Instantiate(enemy) as GameObject;

            //initialize enemy memebers
            float xloc = Random.Range(-5.5f, 5.5f);
            enemyClone.transform.position = (new Vector2(xloc, 5));
            enemyClone.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemyClone.GetComponent<Badguybehavior>().move = false;
            enemyClone.GetComponent<Badguybehavior>().moveRight = (Random.value > .5f);

            //ignore colliders w other bad guys
            Physics2D.IgnoreLayerCollision(8, 8);
            //reset counter
            spawnTimer = spawnTime;
        }

        //can it get any crazier???
        if (spawnTime > .1f)
        {
            //incriment spawn timer
            if (incrimentTimer < 0)
            {
                //new spawn time
                spawnTime = spawnTime - incrimentDelta;

                //reset incriment timer
                incrimentTimer = incrimentTime;
            }
        }
    }

    public void reset()
    {
        spawnTime = 5f;
        spawnTimer = spawnTime;
        incrimentTimer = incrimentTime;
        //while (GameObject.FindGameObjectWithTag("Enemy") != null) Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

}
