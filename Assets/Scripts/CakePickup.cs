using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakePickup : MonoBehaviour
{

    [SerializeField] AudioClip PickupSFX;
    [SerializeField] int livesForPickup = 1;

    bool wasCollected = false; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint( PickupSFX, Camera.main.transform.position );
            FindObjectOfType<GameSession>().AddToLife(livesForPickup);
           
            Destroy(gameObject);
        }
    }
}
