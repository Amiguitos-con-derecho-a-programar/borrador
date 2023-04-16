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
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
       {
         Time.timeScale = 0.5f; // Disminuye la velocidad del juego a la mitad
       }
       else
       {
         Time.timeScale = 1f;
       }
    }
    void Run()
    {
        transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, player.position.x, speed * Time.deltaTime), transform.position.y);

    }
 
}
