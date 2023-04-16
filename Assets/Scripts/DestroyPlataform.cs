using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestroyPlataform : MonoBehaviour
{
    [SerializeField] GameObject plataforma;
    BoxCollider2D myTextCollider;
    public static bool ObjectDestroy = false;
        
    
    void Start()
    {
        myTextCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (myTextCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("tocando");
            presionarBoton(plataforma);
        }
    }

    void presionarBoton(GameObject plataforma)
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("presionando");

            if (ObjectDestroy)
            {
                Resume();
            }
            else
            {
                Destruir(plataforma);
            }
        }
    }
    
    public void Resume()
    {

        ObjectDestroy = false;
    }

    void Destruir(GameObject plataforma)
    {
        Destroy(plataforma);
        ObjectDestroy = true;
    }

}
