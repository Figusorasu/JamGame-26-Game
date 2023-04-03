using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{   
    private PlayerController player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Move(float inputHorizontal) {
        Debug.Log("Moving " + inputHorizontal);
        player.rb.velocity = new Vector2(inputHorizontal * player.speed, player.rb.velocity.y);
        Debug.Log("Dadadadaaaaa");
    }

    public void playerJump() {
        if(player.isGrounded) {
            player.rb.velocity = Vector2.up * player.jumpforce;
        }
    }

}
