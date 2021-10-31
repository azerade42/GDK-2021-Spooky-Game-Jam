using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    [SerializeField] protected float fallSpeed;
    Canvas textCanvas;
    [SerializeField] Animation scoreFade;

    protected void StartCustom()
    {
        textCanvas = GameObject.FindGameObjectWithTag("WorldTextCanvas").GetComponent<Canvas>();
    }

    private void Update()
    {
        MoveDownwards();
    }

    protected virtual void MoveDownwards()
    {
        float yTranslation = -fallSpeed * 0.1f * Time.deltaTime;
        transform.Translate(new Vector3(0, yTranslation, 0));
    }

    protected void RemoveTerrain(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    protected void DestroyOnPlayerTouch(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            gameObject.SetActive(false);

            int scoreToAdd = DetermineScore();

            GameManager.Instance.PlayerScore = scoreToAdd;

            DisplayScorePopup(scoreToAdd);

        }
    }

    private void DisplayScorePopup(int scoreToAdd)
    {
        GameObject obj = ObjectPooler.Instance.SpawnFromPool("PopupScoreText", transform.position - textCanvas.transform.position);
        obj.transform.SetParent(textCanvas.transform, false);
        obj.GetComponent<RectTransform>().position = transform.position;
        obj.GetComponent<Text>().text = "+" + scoreToAdd;
        obj.GetComponent<Text>().color = DetermineColor(scoreToAdd);
    }

    // Determines score based on the height the falling object was collected
    private int DetermineScore()
    {
        int scoreToAdd = 0;

        float Ypos = transform.position.y;

        if (Ypos > 2f)
        {
            scoreToAdd = 100;
        }
        else if (Ypos > 0.5f)
        {
            scoreToAdd = 50;
        }
        else if (Ypos > -1f)
        {
            scoreToAdd = 25;
        }
        else if (Ypos > -2.5f)
        {
            scoreToAdd = 10;
        }
        else if (Ypos > -4f)
        {
            scoreToAdd = 5;
        }
        else
        {
            scoreToAdd = 100;
        }

        return scoreToAdd;
    }

    private Color DetermineColor(int score)
    {
        Color colorToDisplay = Color.white;

        if (score == 100)
        {
            colorToDisplay = Color.red;
        }
        else if (score == 50)
        {
            colorToDisplay = Color.yellow;
        }
        else if (score == 25)
        {
            colorToDisplay = Color.green;
        }
        else if (score == 10)
        {
            colorToDisplay = Color.cyan;
        }

        return colorToDisplay;
    }
}
