using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del objeto
    public bool isBeingCarried = false; // Bandera que indica si el objeto está siendo llevado por el jugador

    private Rigidbody2D rb; // Referencia al Rigidbody2D del objeto
    
    void Start() {
        rb = GetComponent<Rigidbody2D>(); // Obtiene la referencia al Rigidbody2D
    }

    void FixedUpdate() {
        // Si el objeto está siendo llevado por el jugador, se mueve con él
        if (isBeingCarried) {
            rb.MovePosition(rb.position + new Vector2(Input.GetAxis("Horizontal"), 0f) * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
