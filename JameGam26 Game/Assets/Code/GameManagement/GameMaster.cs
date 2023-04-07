using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        Application.targetFrameRate = 60;
    }

    public int currentCoins = 0;
    public int healingCoins = 0;

    public int coinsCollected_Lvl_1 = 0;
    public int coinsCollected_Lvl_2 = 0;
    public int coinsCollected_Lvl_3 = 0;

    public bool showMobileControls = true;

    private void Update() {
        showMobileControls = true;
    }

}