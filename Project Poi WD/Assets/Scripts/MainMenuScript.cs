using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public string newGameScene;

    [Header("Start/Quit Game Ui")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [Header("Settings Panel Ui")]
    public GameObject SettingsPanel;
    [SerializeField] Button openSettingsButton;
    [SerializeField] Button closeSettingsButton;

    [Space]
    [Header("Credits Panel")]
    public GameObject CreditsPanel;
    [SerializeField] Button openCreditsButton;
    [SerializeField] Button closeCreditsButton;

    // Start is called before the first frame update
    void Start()
    {
        // add click events
        // start game
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(startGame);
        // quit game
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame); 
        // settings
        openSettingsButton.onClick.RemoveAllListeners();
        openSettingsButton.onClick.AddListener(openSettings);
        closeSettingsButton.onClick.RemoveAllListeners();
        closeSettingsButton.onClick.AddListener(closeSettings);
        // credits
        openCreditsButton.onClick.RemoveAllListeners();
        openCreditsButton.onClick.AddListener(openCredits);
        closeCreditsButton.onClick.RemoveAllListeners();
        closeCreditsButton.onClick.AddListener(closeCredits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startGame()
    {
        if (!string.IsNullOrEmpty(newGameScene))
        {
            SceneManager.LoadScene(newGameScene);
            Debug.Log("Starting Game!");
        }
        else
        {
            Debug.Log("Please write a scene name in the 'newGameScene' field of the Main Menu Script and don't forget to add that scene in the Build Settings!");
        }
    }

    private void openSettings()
    {
        SettingsPanel.SetActive(true);
        Debug.Log("Open Settings!");
    }

    private void closeSettings()
    {
        SettingsPanel.SetActive(false);
        Debug.Log("Close Settings..");
    }

    private void openCredits()
    {
        CreditsPanel.SetActive(true);
        Debug.Log("Open Credits!");
    }

    private void closeCredits()
    {
        CreditsPanel.SetActive(false);
        Debug.Log("Close Credits!");
    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
				            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
