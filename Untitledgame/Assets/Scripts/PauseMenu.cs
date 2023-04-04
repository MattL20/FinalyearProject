using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Controls the pause menu
public class PauseMenu : MonoBehaviour
{
    
    public static bool isPaused = false;//if the game is paused
    public GameObject pauseMenu;//reference to the pause menu UI
    
    // Update is called once per frame
    void Update()
    {
        //used to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //Disable the pause menu UI and start the game again
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    //Activate the pause menu UI and stop everything in the game
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
