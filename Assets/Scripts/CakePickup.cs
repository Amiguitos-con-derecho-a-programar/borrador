using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakePickup : MonoBehaviour
{

    [SerializeField] int livesForPickup = 1;

    bool wasCollected = false; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToLife(livesForPickup);
           
            Destroy(gameObject);
        }
    }
}
