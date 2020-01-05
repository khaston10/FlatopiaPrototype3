using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuStart : MonoBehaviour
{
    public GameObject SettingsPanel;
    public bool settingsActive = false;


    void Start()
    {
        // Set the settings panel inactive.
        SettingsPanel.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsActive == false)
        {
            SettingsPanel.SetActive(true);
            settingsActive = true;
            GameObject.Find("Game").GetComponent<GameMain>().gamePaused = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && settingsActive == true)
        {
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


    public void ExitGame()
    {
        Application.Quit();
    }
}
