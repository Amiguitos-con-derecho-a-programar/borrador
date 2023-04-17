using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCREAMBOSS : MonoBehaviour
{
    [SerializeField] AudioSource PickupSFX;
    bool wasCollected = false; 
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss" && !wasCollected)
        {
            wasCollected = true;
            PickupSFX.Play();
            
            Destroy(gameObject);
        }
    }
}
