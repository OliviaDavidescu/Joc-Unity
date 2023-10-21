using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitMenu : MonoBehaviour
{
    public string Level;

    private Button exitButton;
    private Button mainButton;
    private Button retryButton;

   

    void Start()
    {
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        mainButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>(); 

        exitButton.onClick.AddListener(ExitGame);
        mainButton.onClick.AddListener(GoToMainMenu);
        retryButton.onClick.AddListener(Retry);

      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Go back to the same exact game");
        }
    }

    void ExitGame()
    {
        
        Application.Quit();
        Debug.Log("Exiting");
    }

    void Retry()
    {
        SceneManager.LoadScene(Level);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuUnity");
    }
}
