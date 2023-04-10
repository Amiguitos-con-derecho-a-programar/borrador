using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class Dialogue03 : MonoBehaviour
{   
    BoxCollider2D myTextCollider;
    

    public GameObject dialogo03;
    private string frase1= "XXX ";
    private string frase2= "YYY ";
    private string frase3= "ZZZ ";
    public bool freezeGame = false;
    
    public TextMeshProUGUI TextDialogue01;
    private int currentIndex = 0;
    [SerializeField] string texto;

    // Start is called before the first frame update
    void Start()
    {
        myTextCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int numeroDialogo = 2;
        myTextCollider.enabled = true;
        if (myTextCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            dialogo03.SetActive(true);
            escribirFrase(texto);
            pararJuego();
        }
        else
        {
            dialogo03.SetActive(false);
        }

        void pararJuego()
        {
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.X))
            {
               
                Time.timeScale = 1f;
                dialogo03.SetActive(false);
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
