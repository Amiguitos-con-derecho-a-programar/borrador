using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class finalPanel : MonoBehaviour 
{
   
    public GameObject panel; // Referencia al panel que quieres mostrar

    [SerializeField] TextMeshProUGUI scoresShow;
    string score = GameSession.saveScore;
    BoxCollider2D myTextCollider;
    GameSession instanciaValorScore;
  
    void Start()
    {
        panel.SetActive(false);
        myTextCollider = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        Debug.Log("El SCORE EN LA OTRA CLASE ES : " + GameSession.saveScore);
        if (myTextCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("El SCORE FINAL ES : " + score);
            scoresShow.text = GameSession.saveScore;
            pararJuego();
            panel.SetActive(true); 
        }
    }

    void pararJuego()
    {
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 1f;
            
            FindObjectOfType<GameSession>().ResetGameSession();
            

        }
    }
}
