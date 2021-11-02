using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    [SerializeField] Button[] buttons;
    [SerializeField] Button backToMenuButton;
    [SerializeField] Text howToPlayText;
    [SerializeField] Image howToPlayBG;

    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        backToMenuButton.gameObject.SetActive(true);
        howToPlayText.gameObject.SetActive(true);
        howToPlayBG.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        backToMenuButton.gameObject.SetActive(false);
        howToPlayText.gameObject.SetActive(false);
        howToPlayBG.gameObject.SetActive(false);

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        
    }
}
