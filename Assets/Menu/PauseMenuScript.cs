using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public static bool IsPaused = false;

    public GameObject PauseMenuUI;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }


    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }


    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }
    

    public void LoadMenu()
    {
        Debug.Log("Menu load");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
