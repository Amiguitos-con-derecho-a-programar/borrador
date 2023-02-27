using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Velocídad en la que se mueve el jugador.
    [SerializeField] float runSpeed = 10f;
    // Velocidad en que se mueve al saltar.
    [SerializeField] float jumpSpeed = 5f;
    // Velocídad al escalar.
    [SerializeField] float climbSpeed = 5f;
    // Variable para guardar la gravedad que hay en el juego.
    [SerializeField] float gravityScaleAtStart;

    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    
    // Variable para guardar el movimiento del jugador.
    Vector2 moveInput;
    // Variable para guardar las físicas.
    Rigidbody2D myRigidbody;
    // Variable guardar las animaciones.
    Animator myAnimator;
    
    BoxCollider2D myFeetCollider;
    
    CapsuleCollider2D myBodyCollider;

    bool isAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        // Instancias
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        // Guardamos el valor de la gravedad que está en las físicas.
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x),1f);

        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasHorizontalSpeed);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
