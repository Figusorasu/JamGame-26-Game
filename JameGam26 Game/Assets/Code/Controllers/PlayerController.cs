using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [SerializeField] private float inputHorizontal;
    [SerializeField] private float inputVertical;
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


    private void Start() {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
    }

    private void Update() {
        if(!facingRight && rb.velocity.x > 0) {
            Flip();
        } else if(facingRight && rb.velocity.x < 0) {
            Flip();
        }
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




}
