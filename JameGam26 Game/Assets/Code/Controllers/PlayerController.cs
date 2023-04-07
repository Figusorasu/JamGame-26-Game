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

    public bool isGrounded;
    

    [Header("Components")]
    private GameMaster GM;

    [SerializeField] private Joystick joystick;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator anim;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private HealthController hp;


    private void Start() {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        GM.currentCoins = 0;
        GM.healingCoins = 0;

        //joystick = GameObject.FindGameObjectWithTag("Joistick").GetComponent<FixedJoystick>();
    }

    private void FixedUpdate() {
        if(canMove) {
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

            if(GM.showMobileControls) {
                if(joystick.Horizontal >= .2f) {
                    inputHorizontal = joystick.Horizontal;
                } else if(joystick.Horizontal <= -.2f) {
                    inputHorizontal = joystick.Horizontal;
                } else {
                    inputHorizontal = 0f;
                }
            }
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

        if(!isGrounded && hp.health != 0) {
            anim.SetBool("Jump", true);
            trail.emitting = true;
        } else {
            anim.SetBool("Jump", false);
            trail.emitting = false;
        }

        if(hp.health < 1) {
            canMove = false;
            canJump = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0,0);
            
            anim.SetTrigger("Death");
            StartCoroutine(Death(1f));
        }

    }

    private IEnumerator Death(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        reloadLevel();
    }

    public void reloadLevel() {
        GM.currentCoins = 0;
        GM.healingCoins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Coin")) {
            other.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Collect");
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("CardPickup");
            Destroy(other.gameObject, 0.5f);
            GM.currentCoins += 1;
            GM.healingCoins += 1;
        }

        if(other.CompareTag("Enemy")) {
            hp.health -= 1;
            if(hp.health > 0) {
                FindObjectOfType<AudioManager>().PlaySound("PlayerHurt");
                anim.SetTrigger("Hurt");
            } else if(hp.health <= 0){
                FindObjectOfType<AudioManager>().PlaySound("PlayerDeath");
            }
        }

        if(other.CompareTag("DeathRegion")) {
            hp.health = 0;
            FindObjectOfType<AudioManager>().PlaySound("PlayerDeath");
        }

        if(other.CompareTag("Finish")) {
            canMove = false;
            canJump = false;
            rb.velocity = new Vector2(0,0);
            other.gameObject.GetComponent<Animator>().SetTrigger("win");
            anim.SetTrigger("win");
            GameObject.FindObjectOfType<UserInterface>().transform.GetChild(8).gameObject.SetActive(true);
            GameObject.FindObjectOfType<UserInterface>().transform.GetChild(7).gameObject.SetActive(false);
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
