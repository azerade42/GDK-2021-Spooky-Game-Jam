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

    public List<GameObject> inactiveTerrains = new List<GameObject>();

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
            scoreText.text = "SCORE: " + playerScore;
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

    public void AddInactiveTerrain(GameObject terrainToAdd)
    {
        inactiveTerrains.Add(terrainToAdd);
    }

    public void RemoveInactiveTerrain(GameObject terrainToRemove)
    {
        inactiveTerrains.Remove(terrainToRemove);
    }
}
