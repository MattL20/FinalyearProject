using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Controls the main menu buttons
public class MenuManager : MonoBehaviour
{
    //Reference to the regular music
   public AudioSource NormalMusic;
    //change the scene by the scen inputted
   public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    //quit the application
    public void Quit()
    {
        Application.Quit();
    }
    //stop the music
    public void StopMusic()
    {
        NormalMusic.Stop();
    }
}
