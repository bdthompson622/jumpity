using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitonhead : MonoBehaviour
{ 
    public AudioClip bopped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            SoundManager.instance.playSingle(bopped);
            //GetComponentInParent<Badguybehavior>().respawn();
            GetComponentInParent<Badguybehavior>().dead = true;
            GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(transform.parent.gameObject);
        }
    }
}
