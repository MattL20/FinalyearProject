using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource NormalMusic;
   public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StopMusic()
    {
        NormalMusic.Stop();
    }
}
