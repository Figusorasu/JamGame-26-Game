using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    private GameMaster GM;

    [SerializeField] TMP_Text coinCounter;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update() {
        if(GM.currentCoins < 10) {
            coinCounter.text = "0" + GM.currentCoins;
        } else if(GM.currentCoins >= 10) {
            coinCounter.text = GM.currentCoins + "";
        }
        
    }
}
