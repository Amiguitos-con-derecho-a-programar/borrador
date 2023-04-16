using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;


public class Dialogues : MonoBehaviour
{
    BoxCollider2D myTextCollider;
    public GameObject dialogo01;
   

    public bool freezeGame = false;

    public TextMeshProUGUI TextDialogue01;
    private int currentIndex = 0;
    [SerializeField] string texto;
    

    void Start()
    {
        myTextCollider = GetComponent<BoxCollider2D>();
      
    }

    // Update is called once per frame

    void Update()
    {
        int numeroDialogo = 1;
        if (myTextCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            dialogo01.SetActive(true);
            Console.WriteLine(numeroDialogo);
            escribirFrase(texto);
            pararJuego();
        }
        else
        {
            dialogo01.SetActive(false);
        }

        void pararJuego()
        {
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.X))
            {
                Time.timeScale = 1f;
                dialogo01.SetActive(false);
             Destroy(gameObject);
               
            }
        }
        

        void escribirFrase(string frase)
        {
            if (currentIndex < frase.Length)
            {
                TextDialogue01.text += frase[currentIndex];                     
                currentIndex++;
            }                                  

        }
    }

  
}
