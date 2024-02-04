using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.OnLivesChanged += ReloadLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReloadLevel(int lives)
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnDisable()
    {
        Player.OnLivesChanged -= ReloadLevel;
    }
}
