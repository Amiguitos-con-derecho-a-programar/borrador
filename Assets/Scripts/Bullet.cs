using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Rigidbody2D myRigidbody2D;
   // PlayerMovement player;
   
    float xSpeed = 1F;
    
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        
        xSpeed = transform.localScale.x * bulletSpeed;
    }
    
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(xSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        } 
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
