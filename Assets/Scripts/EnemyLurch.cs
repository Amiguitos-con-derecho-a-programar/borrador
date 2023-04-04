using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLurch : MonoBehaviour
{
    public float speed;
    public Transform player;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D myRigidbody;
   
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, player.position.x, speed * Time.deltaTime), transform.position.y);

    }

}
