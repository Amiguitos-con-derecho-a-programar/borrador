using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosEventos : MonoBehaviour
{

    [SerializeField] AudioClip PickupSFX;
    bool wasCollected = false;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(PickupSFX, Camera.main.transform.position );
            Destroy(gameObject);
        }
    }
}
