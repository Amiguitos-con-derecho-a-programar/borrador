using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody2D;
    PlayerMovement player;
    float xSpeed;
    
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    
    void Update()
    {
        myRigidbody2D.velocity = new Vector2(xSpeed, 0);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
