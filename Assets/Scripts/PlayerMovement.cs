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
    // [SerializeField] GameObject bullet;
                                        // [SerializeField] Transform gun;
    
    // Empujar objeto
    public float pickupRange = 0.5f; // Rango de distancia en la que el jugador puede recoger el objeto
    public KeyCode pickupButton; // Botón que el jugador debe presionar para recoger y soltar el objeto
    private GameObject carriedObject; // Objeto que el jugador está llevando

    
    
    // Variable para guardar el movimiento del jugador.
    Vector2 moveInput;
    // Variable para guardar las físicas.
    Rigidbody2D myRigidbody;
    // Variable guardar las animaciones.
    Animator myAnimator;
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    bool isAlive = true;
    
    void Start()
    {
        // Instancias
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }
    
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        
        
        if (Input.GetKeyDown(pickupButton) && !carriedObject) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

            foreach (Collider2D collider in colliders) {
                if (collider.CompareTag("Carryable")) {
                    carriedObject = collider.gameObject;
                    carriedObject.GetComponent<moveObject>().isBeingCarried = true;
                    carriedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    carriedObject.transform.parent = transform;
                    break;
                }
            }
        }
        // Si el jugador está llevando un objeto y presiona el botón de soltar, suelta el objeto
        else if (Input.GetKeyDown(pickupButton) && carriedObject) {
            carriedObject.GetComponent<moveObject>().isBeingCarried = false;
            carriedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            carriedObject.transform.parent = null;
            carriedObject = null;
        }
        
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    // void OnFire(InputValue value)
    // {
    //     if (!isAlive)
    //     {
    //         return;
    //     }
    //
    //     Instantiate(bullet, gun.position, transform.rotation);
    // }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        myAnimator.SetBool("isJumping", false);
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Obstacules", "PlataformDestroy", "LurchRel")))
        {
            
            return;
        }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            myAnimator.SetBool("isJumping", true);
            
        }
        else if(!value.isPressed)
        {
            myAnimator.SetBool("isJumping", false);
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
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards", "Boss")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
 

}
