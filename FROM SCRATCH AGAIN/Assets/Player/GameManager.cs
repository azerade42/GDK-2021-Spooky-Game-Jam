using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text scoreText;
    [SerializeField] Text gameOverText;

    private int playerScore;
    private bool gameOver = false;

    public int PlayerScore
    {
        get
        {
            return playerScore;
        }
        set
        {
            playerScore += value;
            print(value);
            scoreText.text = "Score: " + playerScore;
        }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            if (gameOver == true)
            {
                RunGameOver();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void RunGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManager.Instance.RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.QuitGame();
            }
            return;
        }
    }
}
