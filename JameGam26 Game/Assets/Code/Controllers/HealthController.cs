using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Image[] hearts;

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            health -= 1;
        }
    }

}
