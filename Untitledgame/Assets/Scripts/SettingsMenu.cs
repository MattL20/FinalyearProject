using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
//Controls the settings menu UI
public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;//reference to the master audio mixer volume
    Resolution[] resolutions;// used to hold system resolutions available
    public TMP_Dropdown resDropdown;//refernce to the resolution dropdown

    // Start is called before the first frame update
    private void Start()
    {
        //fills the resolution dropdown with the resoltions available on the system
        if (resDropdown != null)
        {
            resolutions = Screen.resolutions;
            resDropdown.ClearOptions();
            List<string> resOptions = new List<string>();
            int currentResIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].ToString();
                resOptions.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResIndex = i;
                }
            }

            resDropdown.AddOptions(resOptions);
            resDropdown.value = currentResIndex;
            resDropdown.RefreshShownValue();
        }
    }
    //sets the volume when the volume slider is changed
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    //sets the graphic quality when quality is chosen from the dropdown
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    //sets the game to fullscreen
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    //sets the games resolution
    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
