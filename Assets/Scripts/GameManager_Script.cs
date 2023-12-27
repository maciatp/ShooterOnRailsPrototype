using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Script : MonoBehaviour
{
    static bool isGamePaused = false;
    
    [Header("UI GameObjects")]
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameUI;

    [Header("UI Buttons")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button quitButton;

   
    


    private void Start()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       

        if(isGamePaused == true)
        {
            //resumeButton.(); HIGHLIGHT BUTTON
        }
       
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        gameUI.SetActive(false);
        isGamePaused = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        gameUI.SetActive(true);
        isGamePaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        gameUI.SetActive(true);
        isGamePaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu"); // para cuando tenga menú
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("He quitado el juego pero en el editorn no pasará nada");
    }



}
