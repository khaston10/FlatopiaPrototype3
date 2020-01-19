using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Variables
    public int worldSize;
    public int foodSpawned;
    public int plantEaters;
    public int gamePoints;
    public int day;


    public bool landOwnerAchievement;
    public bool genocideAchievement;
    public bool ninjaAchievement;
    public bool survivalistAchievement;
    public bool glutonAchievement;
    public bool unlockedAchievement;
    #endregion

    #region Functions

    // Deals with the Start Button.
    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName: "Level01");
    }

    public void ClickSettings()
    {
        Debug.Log("Settings");
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

    #endregion

}
