using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TouchPhase = UnityEngine.TouchPhase;

public class UserInterface : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject levelCompleateUI;
    public GameObject mobileControls;

    private GameMaster gm;

    private void Start() {
        Resume();
        levelCompleateUI.SetActive(false);

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update() {
        if(gm.showMobileControls == true) {
            mobileControls.SetActive(true);
        } else {
            mobileControls.SetActive(false);
        }
    }


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

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
