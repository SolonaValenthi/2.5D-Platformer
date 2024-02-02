using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnCollectCoin += UpdateCoins;
    }

    private void UpdateCoins(int coins)
    {
        _coinText.text = $"Coins: {coins}";
    }
}
