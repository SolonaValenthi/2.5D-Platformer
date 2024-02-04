using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _livesText;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnCollectCoin += UpdateCoins;
        Player.OnLivesChanged += UpdateLives;
    }

    private void UpdateCoins(int coins)
    {
        _coinText.text = $"Coins: {coins}";
    }

    private void UpdateLives(int lives)
    {
        _livesText.text = $"Lives: {lives}";
    }

    private void OnDisable()
    {
        Player.OnCollectCoin -= UpdateCoins;
        Player.OnLivesChanged -= UpdateLives;
    }
}
