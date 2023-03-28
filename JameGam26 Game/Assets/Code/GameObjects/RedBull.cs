using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBull : MonoBehaviour
{
    public float speed;
    public bool moveHorizontally = true;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool facingRight = false;
    
    private float horizontal = 1f;
    private float vertical = 1f;
    
    private void FixedUpdate()
    {
        if(moveHorizontally) {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        

        if(!facingRight && rb.velocity.x > 0) {
            Flip();
        } else if(facingRight && rb.velocity.x < 0) {
            Flip();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("DirectionFlipper")) {
            horizontal *= -1;
            vertical *= -1;
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
