using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedController : MonoBehaviour
{
    #region Variables

    #region Panels
    public GameObject statsPanel;
    public GameObject upgradesPanel;
    public GameObject achievementPanel;
    public GameObject gunnerPanel;
    public GameObject builderPanel;
    public GameObject saboteurPanel;
    public GameObject outpostPanel;
    public GameObject regionPanel;
    #endregion

    #region Texts
    public Text UpgradeText;
    public Text AchievementText;
    public Text RegionText;

    #endregion

    #region Misc
    public AudioClip ClickButton;
    #endregion

    #region Buttons
    public Button statsPanelButton;
    public Button upgradesPanelButton;
    public Button achievementPanelButton;
    public Button gunnerPanelButton;
    public Button builderPanelButton;
    public Button saboteurPanelButton;
    public Button outpostPanelButton;
    public Button regionPanelButton;
    #endregion


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel(int panelNumber)
    {
        HidePanels();
        if (panelNumber == 0) statsPanel.SetActive(true);
        else if (panelNumber == 1) upgradesPanel.SetActive(true);
        else if (panelNumber == 2) achievementPanel.SetActive(true);
        else if (panelNumber == 3) gunnerPanel.SetActive(true);
        else if (panelNumber == 4) builderPanel.SetActive(true);
        else if (panelNumber == 5) saboteurPanel.SetActive(true);
        else if (panelNumber == 6) outpostPanel.SetActive(true);
        else regionPanel.SetActive(true);

    }

    public void HidePanels()
    {
        statsPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        achievementPanel.SetActive(false);
        gunnerPanel.SetActive(false);
        builderPanel.SetActive(false);
        saboteurPanel.SetActive(false);
        outpostPanel.SetActive(false);
        regionPanel.SetActive(false);
    }

    #region Upgrades Functions

    public void ClickUpgrade(int i)
    {
        if(GameObject.Find("Game").GetComponent<GameMain>().gamePoints >= 10)
        {
            AudioSource.PlayClipAtPoint(ClickButton, transform.position);
            GameObject.Find("Game").GetComponent<GameMain>().UpgradesUpdated(i);
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints -= 10;

            if (i == 0) GameObject.Find("Game").GetComponent<GameMain>().slayerUpgradeUnlocked = true;
            else if (i == 1) GameObject.Find("Game").GetComponent<GameMain>().freezeUpgradeUnlocked = true;
            else if (i == 2) GameObject.Find("Game").GetComponent<GameMain>().feedUpgradeUnlocked = true;
            else if (i == 3) GameObject.Find("Game").GetComponent<GameMain>().caffeineUpgradeUnlocked = true;
        }
    }

    public void HoverSlayer()
    {
        UpgradeText.text = "SLAYER:\n\nKills all Meat-Eaters\n\nCost: 10";
    }

    public void HoverFreeze()
    {
        UpgradeText.text = "FREEZE:\n\nFreezes Meat-Eaters\n\nCost: 10";
    }

    public void HoverFeed()
    {
        UpgradeText.text = "FEED:\n\nFeeds all Plant-Eaters\n\nCost: 10";
    }

    public void HoverCaffeine()
    {
        UpgradeText.text = "CAFFEINE:\nTemporaly increases Plant-Eater speed\n\nCost: 10";
    }


    public void TextClear()
    {
        UpgradeText.text = "";
    }


    #endregion

    #region Achievement Functions

    public void ClickAchievement(int achievementNumber)
    {
        AudioSource.PlayClipAtPoint(ClickButton, transform.position);
        if (achievementNumber == 0) AchievementText.text = "NINJA:\nNo Plant-Eaters killed by Meat-Eaters for 10 or more days.";
        else if (achievementNumber == 1) AchievementText.text = "GENOCIDE:\nKill 5 or more Meat-Eaters in 1 day.";
        else if (achievementNumber == 2) AchievementText.text = "SURVIVALIST:\nSurvive for more than 40 days.";
        else if (achievementNumber == 3) AchievementText.text = "LAND-OWNER:\nWorld size of 30 or more.";
        else if (achievementNumber == 4) AchievementText.text = "GLUTON:\n1 Plant-Eater eats 5 or more plants in 1 day.";
        else if (achievementNumber == 5) AchievementText.text = "UNLOCKED:\nAll other achievments unlocked.";
    }

    #endregion

    #region Region Functions

    public void ClickRegion(int regionNum)
    {
        //0: Plains, 1: Desert, 2: Jungle, 3: Tundra
        GameObject.Find("Game").GetComponent<GameMain>().RegionUpdate(regionNum);
        GameObject.Find("Grid").GetComponent<CreateGrid>().CreateNewRegion(regionNum);
    }

    #endregion
}
