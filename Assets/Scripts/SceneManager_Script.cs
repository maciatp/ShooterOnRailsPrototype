using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        Debug.Log("He clicado el botón");
    }

    public void LoadTestScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("testScene");
    }
    public void LoadShootingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("shootingScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
