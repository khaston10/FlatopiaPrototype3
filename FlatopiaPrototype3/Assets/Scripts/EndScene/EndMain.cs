using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMain : MonoBehaviour
{
    public bool gameWon;
    public int worldSize;
    public int day;
    public int peakPop;
    public bool landOwnerAchievement;
    public bool genocideAchievement;
    public bool ninjaAchievement;
    public bool survivalistAchievement;
    public bool glutonAchievement;
    public bool unlockedAchievement;
    

    public RawImage landOwnerImage;
    public Texture landOwnerTexture;
    public RawImage genocideImage;
    public Texture genocideTexture;
    public RawImage ninjaImage;
    public Texture ninjaTexture;
    public RawImage survivalistImage;
    public Texture survivalistTexture;
    public RawImage glutonImage;
    public Texture glutonTexture;
    public RawImage unlockedImage;
    public Texture unlockedTexture;
    public Text dayText;
    public Text titleText;
    public Text bodyText;
    public Text peakPopText;

    // Start is called before the first frame update
    void Start()
    {
        worldSize = GlobalControl.Instance.worldSize;
        day = GlobalControl.Instance.day;
        peakPop = GlobalControl.Instance.peakPop;
        gameWon = GlobalControl.Instance.gameWon;
        landOwnerAchievement = GlobalControl.Instance.landOwnerAchievement;
        genocideAchievement = GlobalControl.Instance.genocideAchievement;
        ninjaAchievement = GlobalControl.Instance.ninjaAchievement;
        survivalistAchievement = GlobalControl.Instance.survivalistAchievement;
        glutonAchievement = GlobalControl.Instance.glutonAchievement;
        unlockedAchievement = GlobalControl.Instance.unlockedAchievement;

        dayText.text = day.ToString();
        peakPopText.text = peakPop.ToString();

        if (gameWon)
        {
            titleText.text = "CONGRATULATIONS";
            bodyText.text = "Thank you for helping the Plant-Eaters survive.\n\nThe adventure will continue soon!";
            dayText.text = "";
        }

        // Load Achievement Icons.
        if (landOwnerAchievement)
        {
            landOwnerImage.color = Color.white;
            landOwnerImage.texture = landOwnerTexture;
        }
        if (genocideAchievement)
        {
            genocideImage.color = Color.white;
            genocideImage.texture = genocideTexture;
        }
        if (ninjaAchievement)
        {
            ninjaImage.color = Color.white;
            ninjaImage.texture = ninjaTexture;
        }
        if (survivalistAchievement)
        {
            survivalistImage.color = Color.white;
            survivalistImage.texture = survivalistTexture;
        }
        if (glutonAchievement)
        {
            glutonImage.color = Color.white;
            glutonImage.texture = glutonTexture;
        }
        if (unlockedAchievement)
        {
            unlockedImage.color = Color.white;
            unlockedImage.texture = unlockedTexture;
        }

        

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(sceneName: "StartScreen");
    }



    
}
