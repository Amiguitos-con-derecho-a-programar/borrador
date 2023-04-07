using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using UnityEngine.Networking;
using System.Net;
using System.IO;
using System.Text;

public class GameSession : MonoBehaviour
{
   
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText; 
    [SerializeField] TextMeshProUGUI scoresText;
    
    [SerializeField] TextMeshProUGUI livesTextC; 
    [SerializeField] TextMeshProUGUI scoresTextF;


    
    public GameObject canvasMenuP;
    public GameObject canvasMenuC;
    public GameObject canvasMenuF;
    
    public GameObject destornillador;
    public GameObject palanca;

    bool estasVivo = false;
    bool estasMuerto = false;



    
    void Awake()
    {
       
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
        
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        canvasMenuC.SetActive(false);
        canvasMenuF.SetActive(false);
        destornillador.SetActive(false);
        palanca.SetActive(false);
        Update();
     
        livesText.text = playerLives.ToString();
        scoresText.text = score.ToString();

    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
           
            var current = score.ToString();            
            UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/send", current);
            www.Send();
            estasVivo = false;
            estasMuerto = true;
            canvasMenuF.SetActive(true);
            Update();
            
            
        }
    }

    void ResetGameSession()
    {
       FindObjectOfType<ScenePersist>().ResetScenePersist();
       SceneManager.LoadScene(0);
       Destroy(gameObject);
       
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoresText.text = score.ToString();
        scoresTextF.text = score.ToString();
    }

    public void AddToLife(int livesToAdd)
    {
        playerLives += livesToAdd;
        livesText.text = playerLives.ToString();
        livesTextC.text = playerLives.ToString();
    }

    void TakeLife()
    {
        print("moriste");
        canvasMenuC.SetActive(true);
        Update();
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
        livesTextC.text = playerLives.ToString();

    }

    void menuPrincipal()
    {
        Update();
    }

    void Update()
    {
        print("iniciando");
        if (estasMuerto == false && estasVivo == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                estasVivo = true;
                canvasMenuP.SetActive(false);
               
            }
        }

        if (estasMuerto == false && estasVivo == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
            
                canvasMenuC.SetActive(false);
               
            }
        }

        if (estasMuerto == true && estasVivo == false)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
            
                canvasMenuF.SetActive(false);
                ResetGameSession();
            }
        }
    }
}
