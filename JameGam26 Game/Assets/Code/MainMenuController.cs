using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameMaster gm;

    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void SelectLevel1() {
        SceneManager.LoadScene("Level1");
    }

    public void SelectLevel2() {
        SceneManager.LoadScene("Level2");
    }

    public void SelectLevel3() {
        SceneManager.LoadScene("Level3");
    }


    public void PlayClickSound() {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
    }

    public void PlayHoverSound() {
        FindObjectOfType<AudioManager>().PlaySound("ButtonHover");
    }

    public void showMobileControlsUI() {
        gm.showMobileControls = !gm.showMobileControls;
    }


}
