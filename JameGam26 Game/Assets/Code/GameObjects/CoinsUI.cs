using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    private GameMaster GM;
    private GameObject[] allCoins;
    private int numOfAllCoins;

    [SerializeField] TMP_Text coinCounter;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        allCoins = GameObject.FindGameObjectsWithTag("Coin");
        numOfAllCoins = allCoins.Length;
        
    }

    private void Update() {
        if(GM.currentCoins < 10) {
            coinCounter.text = "0" + GM.currentCoins + "/" + numOfAllCoins;
        } else if(GM.currentCoins >= 10) {
            coinCounter.text = GM.currentCoins + "/" + numOfAllCoins;
        }
        
    }
}
