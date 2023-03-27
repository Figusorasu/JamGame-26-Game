using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public bool canMove = true;

    private float inputHorizontal;
    private float inputVertical;
    private bool facingRight = true;

    [Header("Jump")]
    public bool canJump;
    public float jumpforce;
    
    [Header("Ground Detection")]
    public float checkRadius;

    [SerializeField] private LayerMask whatIsGround;

    private bool isGrounded;
    

    [Header("Components")]
    private GameMaster GM;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator anim;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private HealthController hp;


    private void Start() {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void FixedUpdate() {
        if(canMove) {
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
        }
        
        if(!facingRight && rb.velocity.x > 0) {
            Flip();
        } else if(facingRight && rb.velocity.x < 0) {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded) {
            canJump = true;
        } else {
            canJump = false;
        }

        if(rb.velocity.x == 0) {
            anim.SetBool("Walk", false);
        } else {
            anim.SetBool("Walk", true);
        }

        if(rb.velocity.y == 0) {
            anim.SetBool("Jump", false);
            trail.emitting = false;
        } else {
            anim.SetBool("Jump", true);
            trail.emitting = true;
        }


        if(hp.health == 0) {
            
            GM.currentCoins = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Coin")) {
            Destroy(other.gameObject);
            GM.currentCoins += 1;
        }

        if(other.CompareTag("Enemy")) {
            hp.health -= 1;
        }
    }

    private void OnDrawGizmosSelected() {
        // Ground Check Radius Display
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }


    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Move(InputAction.CallbackContext ctx) {
        inputVertical = ctx.ReadValue<Vector2>().y;
        inputHorizontal = ctx.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if(ctx.performed && canJump) {
            rb.velocity = Vector2.up * jumpforce;
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        if(ctx.performed) {

        }
    }


}
