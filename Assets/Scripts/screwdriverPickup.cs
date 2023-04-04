using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwdriverPickup : MonoBehaviour
{
    public GameObject destornilladorIMG;
    bool wasCollected = false;
    bool llaveSalidaDest = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            Destroy(gameObject);
            destornilladorIMG.SetActive(true);
            llaveDes();
        }
    }

    void llaveDes()
    {
        llaveSalidaDest = true;
    }
}
