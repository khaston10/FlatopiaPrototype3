using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public int worldSize;
    public int foodSpawned;
    public int plantEaters;
    public int gamePoints;
    public int day;

    private int minWorldSize = 5;
    private int minFoodSpawned = 5;
    private int minPlantEaters = 1;

    public Text worldSizeText;
    public Text foodSpawnedText;
    public Text plantEatersText;
    public Text gamePointsText;
    public Text achievementInformationText;

    public bool landOwnerAchievement;
    public bool genocideAchievement;
    public bool ninjaAchievement;
    public bool survivalistAchievement;
    public bool glutonAchievement;
    public bool unlockedAchievement;

    // Start is called before the first frame update
    void Start()
    {
        worldSize = GlobalControl.Instance.worldSize;
        foodSpawned = GlobalControl.Instance.foodSpawned;
        plantEaters = GlobalControl.Instance.plantEaters;
        gamePoints = GlobalControl.Instance.gamePoints;
        day = GlobalControl.Instance.day;
        landOwnerAchievement = GlobalControl.Instance.landOwnerAchievement;
        genocideAchievement = GlobalControl.Instance.genocideAchievement;
        ninjaAchievement = GlobalControl.Instance.ninjaAchievement;
        survivalistAchievement = GlobalControl.Instance.survivalistAchievement;
        glutonAchievement = GlobalControl.Instance.glutonAchievement;
        unlockedAchievement = GlobalControl.Instance.unlockedAchievement;

    }

    // Update is called once per frame
    void Update()
    {
        worldSizeText.text = worldSize.ToString();
        foodSpawnedText.text = foodSpawned.ToString();
        plantEatersText.text = plantEaters.ToString();
        gamePointsText.text = gamePoints.ToString();
    }

    // Save data to Global Control
    public void SaveData()
    {
        GlobalControl.Instance.worldSize = worldSize;
        GlobalControl.Instance.foodSpawned = foodSpawned;
        GlobalControl.Instance.plantEaters = plantEaters;
        GlobalControl.Instance.gamePoints = gamePoints;
        GlobalControl.Instance.day = day;
        GlobalControl.Instance.landOwnerAchievement = landOwnerAchievement;
        GlobalControl.Instance.ninjaAchievement = ninjaAchievement;
        GlobalControl.Instance.genocideAchievement = genocideAchievement;
        GlobalControl.Instance.glutonAchievement = glutonAchievement;
        GlobalControl.Instance.unlockedAchievement = unlockedAchievement;
        GlobalControl.Instance.survivalistAchievement = survivalistAchievement;
    }

    // Deals with the Start Button.
    public void ClickStart()
    {
        SaveData();
        SceneManager.LoadScene(sceneName: "Level01");
    }

    // Deals with the world size buttons.
    public void ClickSizePlus()
    {
        if (gamePoints > 0)
        {
            worldSize += 1;
            gamePoints -= 1;
        }
    }

    public void ClickSizeMinus()
    {
        if (worldSize > minWorldSize)
        {
            worldSize -= 1;
            gamePoints += 1;
        }
    }

    // Deals with the food spawned buttons.
    public void ClickFoodPlus()
    {
        if (gamePoints > 0 && foodSpawned < worldSize * worldSize)
        {
            foodSpawned += 1;
            gamePoints -= 1;
        }
    }

    public void ClickFoodMinus()
    {
        if (foodSpawned > minFoodSpawned)
        {
            foodSpawned -= 1;
            gamePoints += 1;
        }
    }

    // Deals with the plant eater buttons.
    public void ClickEaterPlus()
    {
        if (gamePoints > 0)
        {
            plantEaters += 1;
            gamePoints -= 1;
        }
    }

    public void ClickEaterMinus()
    {
        if (plantEaters > minPlantEaters)
        {
            plantEaters -= 1;
            gamePoints += 1;
        }
    }

    public void ClickPlantEaterGloss()
    {
        SceneManager.LoadScene("GlossaryPlantEater");
    }

    public void ClickMeatEaterGloss()
    {
        SceneManager.LoadScene("GlossaryMeatEater");
    }

    public void ClickObjective()
    {
        achievementInformationText.text = "OBJECTIVE: Help the Plant-Eaters in their struggle for survival. \n\n HINT: Unlocking Achievments is good for the soul!";
    }

    public void ClickUpgrade()
    {
        achievementInformationText.text = "UPGRADE: Slayer - Eliminates all Meat Eaters. Freeze - Temporarily freezes Meat Eaters until the end of the day.";
    }

    public void ClickNinjaAchievement()
    {
        achievementInformationText.text = "Ninja: No Plant-Eaters killed by Meat-Eaters in a 10 days span";
    }

    public void ClickLandOwnerAchievement()
    {
        achievementInformationText.text = "Land Owner: World size is 30 or greater.";
    }

    public void ClickGenocideAchievement()
    {
        achievementInformationText.text = "Geonocide: 5 or more meat eaters killed in 1 day";
    }

    public void ClickSurvivalistAchievement()
    {
        achievementInformationText.text = "Survivalist: Plant Eaters must survive 40 days.";
    }

    public void ClickGlutonAchievement()
    {
        achievementInformationText.text = "Gluton: A PlantEater must eat 5 or more pants in one day.";
    }

    public void ClickUnlockedAchievement()
    {
        achievementInformationText.text = "Unlocked: Unlock all upgrades.";
    }

    public void ClickSlayerUpgrade()
    {
        achievementInformationText.text = "Slayer: Kill all Meat Eaters on Flatopia.";
    }

    public void ClickFreezeUpgrade()
    {
        achievementInformationText.text = "Freeze: Freezes Meat Eaters for the rest of the day.";
    }

    public void ClickFeedUpgrade()
    {
        achievementInformationText.text = "Feed: Instantly feed all Plant Eaters.";
    }

    public void ClickCaffeineUpgrade()
    {
        achievementInformationText.text = "Caffeine : Plant Eaters move 3x faster for the rest of the day.";
    }

}
