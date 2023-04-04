using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//This script ensures that the audio source does not restart on every scene transition
public class DoNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusicNormal");
        if (musicObj.Length > 1)//if there is more than one audio source with the same name destroy the original audio source
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
