using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPickup : MonoBehaviour
{
    public GameObject palancaIMG;
    bool wasCollected = false;
    bool llaveSalidaPalanca = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            Destroy(gameObject);
            palancaIMG.SetActive(true);
            llaveSalidaPalanca = true;
            
        }
    }
}
