using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController: MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GameOptions()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
