using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;

    // Start is called before the first frame update
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
            Destroy(gameObject);

        else
            DontDestroyOnLoad(gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if (PlayerLives > 1)
            TakeLife();

        else
            ResetGameSession();
    }

    void TakeLife()
    {
        PlayerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene("Level 1");
        Destroy(gameObject);
    }
}
