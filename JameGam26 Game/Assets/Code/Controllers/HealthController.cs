using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Image[] hearts;

    [SerializeField] private Animator healPrompt;

    private GameMaster GM;

    private void Start() {
        health = maxHealth;
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
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

        if(GM.healingCoins == 10) {
            if(health < maxHealth) {
                healPrompt.SetTrigger("Heal");
                FindObjectOfType<AudioManager>().PlaySound("Heal");
                health += 1;
            }
            GM.healingCoins = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            health -= 1;
        }
    }

}
