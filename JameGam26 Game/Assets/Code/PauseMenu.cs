using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public void pauseMenu(InputAction.CallbackContext ctx) {
        if(ctx.performed) {
             if(GameIsPaused) {
                Resume();
             } else {
                Pause();
             }
        }
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetLevel() {
        Resume();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().reloadLevel();
    }

    public void PlayClickSound() {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }

    public void PlayHoverSound() {
        FindObjectOfType<AudioManager>().PlaySound("ButtonHover");
    }
}
