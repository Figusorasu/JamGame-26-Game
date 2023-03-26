using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class NpcController : MonoBehaviour
{
    public string npcName;
    public string dialog;
    
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject talkPrompt;
    [SerializeField] private PlayerController player;

    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            talkPrompt.SetActive(true);
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            talkPrompt.SetActive(false);
            playerInRange = false;
        }
    }

    public void Interaction() {
        if(dialogBox.activeInHierarchy) {
            dialogBox.SetActive(false);
            talkPrompt.SetActive(true);
        } else {
            dialogBox.SetActive(true);
            talkPrompt.SetActive(false);
            dialogText.text = dialog;
        }
    }

    public void Interact(InputAction.CallbackContext ctx) {
        if(ctx.performed && playerInRange) {
            Interaction();
        }
    }
}
