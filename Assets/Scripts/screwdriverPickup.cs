using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screwdriverPickup : MonoBehaviour
{
    public GameObject destornilladorIMG;
    bool wasCollected = false;
    bool llaveSalidaDest = false;
    [SerializeField] AudioClip PickupSFX;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint( PickupSFX, Camera.main.transform.position );
            destornilladorIMG.SetActive(true);
            pararJuego();
        }
    }

    void pararJuego()
        {
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.X))
            {
                Time.timeScale = 1f;
            }
        }   
}
