using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerspawn : MonoBehaviour
{
    public GameObject player;
    public bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isPlayer)
        {
            GameObject playerclone = Instantiate(player) as GameObject;
            playerMade();
            playerclone.transform.Translate(new Vector2(-1.25f, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawn()
    {
        if (!isPlayer)
        {
            GameObject playerclone = Instantiate(player) as GameObject;
            playerMade();
            playerclone.transform.Translate(new Vector2(-1.25f, 0f));
        }
    }

    void playerMade()
    {
        isPlayer = true;
    }

    public void playerDied()
    {
        isPlayer = false;
    }
}
