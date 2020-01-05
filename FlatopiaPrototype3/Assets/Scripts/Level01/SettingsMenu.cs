using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Dropdown resolutionDropDown;
    public GameObject SettingsPanel;
    public bool settingsActive = false;
    Resolution[] resoltuions;
    

    void Start()
    {
        // Set the settings panel inactive.
        SettingsPanel.SetActive(false);

        // Get avaiable resolutions.
        resoltuions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        

        int currentResolutionIndex = 0;

        for (int i = 0; i < resoltuions.Length; i++)
        {
            string option = resoltuions[i].width + "x" + resoltuions[i].height;
            options.Add(option);

            if (resoltuions[i].width == Screen.currentResolution.width && 
                resoltuions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Delete duplicate resolutions.
        options = options.Distinct().ToList();

        // Add options to the drop down.
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && settingsActive == false){
            SettingsPanel.SetActive(true);
            settingsActive = true;
            GameObject.Find("Game").GetComponent<GameMain>().gamePaused = true;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && settingsActive == true){
            SettingsPanel.SetActive(false);
            settingsActive = false;
            GameObject.Find("Game").GetComponent<GameMain>().gamePaused = false;
        }
    }

    public void ExitPanel()
    {
        SettingsPanel.SetActive(false);
        settingsActive = false;
        GameObject.Find("Game").GetComponent<GameMain>().gamePaused = false;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resoltuions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float vol)
    {
        mixer.SetFloat("Volume", vol);

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
