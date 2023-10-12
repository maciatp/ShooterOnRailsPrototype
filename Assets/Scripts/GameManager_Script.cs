using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Script : MonoBehaviour
{//Pause_Script
   public PlayerControls controls;

    public static bool isGamePaused = false;

    public GameObject pauseUI;
    public GameObject gameUI;

    public PlayerMovement_Script playerMovement_Script_;
    public Button resumeButton;
    public Button restartButton;
    public Button menuButton;
    public Button quitButton;
    public Scene currentScene;

   
    private void Awake()
    {
        
        currentScene = SceneManager.GetActiveScene();
        playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();
        gameUI = this.transform.GetChild(0).gameObject;
        pauseUI = this.transform.GetChild(1).gameObject;
        resumeButton = pauseUI.transform.GetChild(0).gameObject.GetComponent<Button>();
        restartButton = pauseUI.transform.GetChild(1).gameObject.GetComponent<Button>();
        menuButton = pauseUI.transform.GetChild(2).gameObject.GetComponent<Button>();
        quitButton = pauseUI.transform.GetChild(3).gameObject.GetComponent<Button>();

        //controls = playerMovement_Script_.gameObject.GetComponent<PlayerShooting_Script>().controls;

        controls = new PlayerControls();

        controls.Gameplay.Pause.performed += ctx =>
        {
            Debug.Log("He pulsado Start");
            if (isGamePaused == true)
            {
                ResumeGame();
                Debug.Log("he pausado el juego");
            }
            else
            {
                PauseGame();
                Debug.Log("he reanudado el juego");
            }
        };

    }


    private void Start()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Start"))
        //{
        //   if (isGamePaused == true)
        //    {
        //        ResumeGame();
        //    }
        //    else
        //    {
        //        PauseGame();
        //    }
        //}

        if((isGamePaused == true) && (playerMovement_Script_.usesJoystick == true))
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
        SceneManager.LoadScene(currentScene.ToString());
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
