using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLoop : MonoBehaviour
{
    float restartTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(GameObject.FindGameObjectWithTag("Respawn").GetComponent<Playerspawn>().isPlayer))
        {
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
}
