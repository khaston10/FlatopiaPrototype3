using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeller : MonoBehaviour
{
    public GameObject storyTellingPanel;
    public Text storyText;
    public Button yesButton;
    public Button noButton;
    public Button gotItButton;

    public bool yes = false;
    public bool no = false;
    public bool gotIt = false;

    public bool tutorialOn = true;
    bool askToStartTutorial = true;
    bool askUserToOperateCamera = false;
    public bool askUserToFindTheDay = false;
    bool askUserToUseTheControlPanel = false;
    public bool askUserToFindGamePoints = false;
    bool askToUseTheSpeedPanel = false;
    public bool askUserToUseSettings = false;
    public bool askUserToLearnAboutMeat = false;
    bool askUserToLEarnAboutMEatCont = false;
    public bool endTutorial = false;
    public bool MeatEatersUpgradeSpeed = false;
    public bool MeatEatersUpgradeVision = false;

    private int plantEaters;
    private int foodSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (askToStartTutorial)
        {
            // Hide Buttons.
            gotItButton.gameObject.SetActive(false);

            // Pause Game.
            PauseGame();

            if (no)
            {
                no = false;
                tutorialOn = false;
                askToStartTutorial = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }

            else if (yes)
            {
                yes = false;
                UnhideButtons();
                askToStartTutorial = false;
                askUserToOperateCamera = true;
            }
        }
        
        else if (askUserToOperateCamera)
        {
            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            UpdatePanel("This is FLATOPIA! \n\nA peaceful, flat planet inhabited by Plant-Eaters.\n\nTake a look around:\n\nUse the middle mouse button to rotate your view and W, A, S, D to move your view." +
                "\n\nAt anytime you can reset the view by pressing C.");

            if (gotIt)
            {
                gotIt = false;
                askUserToOperateCamera = false;           
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }
        }

        else if (askUserToFindTheDay)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false)
            storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("One day just passed.\n\nYou can see what day it is at the bottom of your screen.");

            if (gotIt)
            {
                gotIt = false;
                askUserToFindTheDay = false;
                plantEaters = GameObject.Find("Game").GetComponent<GameMain>().plantEaters;
                foodSpawned = GameObject.Find("Game").GetComponent<GameMain>().foodSpawned;
                askUserToUseTheControlPanel = true;
            }
        }

        else if (askUserToUseTheControlPanel)
        {
            // Hide Buttons.
            gotItButton.gameObject.SetActive(false);

            UpdatePanel("You currently have " + plantEaters.ToString() + " Plant-Eater(s) and "+ foodSpawned.ToString() + 
                " plant(s).\n\nMake sure the population of Plant-Eaters never reaches 0! Otherwise the Plant-Eaters will go" +
                " extinct and your game will be at an end.\n\nUse the Control Panel to add Plant-Eaters and Plants to the world.\n\nNote: Plants will not grow until sunrise.");

            if (foodSpawned != GameObject.Find("Game").GetComponent<GameMain>().foodSpawned && plantEaters != GameObject.Find("Game").GetComponent<GameMain>().plantEaters)
            {
                askUserToUseTheControlPanel = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }
        }

        else if (askUserToFindGamePoints)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("When a Plant-Eater eats a plant you get a game point.\n\nYou will use game points to purchase items. SAVE THEM UP!\n\nYou can find your current game points on the Control Panel.");

            if (gotIt)
            {
                gotIt = false;
                askUserToFindGamePoints = false;
                askToUseTheSpeedPanel = true;
            }
        }

        else if (askToUseTheSpeedPanel)
        {
            // Hide Buttons.
            gotItButton.gameObject.SetActive(false);

            UpdatePanel("The Speed Panel is located at the top of the screen.\n\nHere you can pause and " +
                "speed up time.\n\nCurrently the game is paused.\n\nPress play or fast-forward to continue.");

            if (GameObject.Find("Game").GetComponent<GameMain>().gamePaused == false)
            {
                askToUseTheSpeedPanel = false;
                UnhideButtons();
                ExitPanel();
            }
        }

        else if (askUserToUseSettings)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            gotItButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("At anytime you can press ESC to get to the Settings Panel.\n\nPress ESC now.");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                askUserToUseSettings = false;
                UnhideButtons();
                ExitPanel();
            }
        }

        else if (askUserToLearnAboutMeat)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("MEAT-EATERS HAVE ARRIVED!\n\nThese ruthless barbarians want nothing more than to snack on Plant-Eaters.\n\n" +
                "Meat-Eaters will starve to death if they can't find food.");

            if (gotIt)
            {
                askUserToLearnAboutMeat = false;
                askUserToLEarnAboutMEatCont = true;
                gotIt = false;
                
            }

        }

        else if (askUserToLEarnAboutMEatCont)
        {
            UpdatePanel("You can also you the Upgrade Panel to help protect against Meat-Eaters.\n\nUpgrades have a steep unlock cost, but once they are " +
                "unlocked you can use them again and again, as long as the timer has re-set.");

            if (gotIt)
            {
                gotIt = false;
                askUserToLEarnAboutMEatCont = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }
        }

        else if (endTutorial)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("End of Tutorial.\n\nNow that you know the basics, help the Plant-Eaters to survive!\n\nTip:\n\n-Unlock Upgrades and Achievements to progress.");

            if (gotIt)
            {
                gotIt = false;
                endTutorial = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }

        }

        else if (MeatEatersUpgradeSpeed)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("Meat-Eaters Upgrade: Speed Increased\n\nCurrent Speed Level: " + GameObject.Find("Game").GetComponent<GameMain>().meatEaterSpeedLevel.ToString()
                + "\n\nCurrent Vision Level: " + GameObject.Find("Game").GetComponent<GameMain>().MeatEaterVisionDistanceLevel.ToString());

            if (gotIt)
            {
                gotIt = false;
                MeatEatersUpgradeSpeed = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }
        }

        else if (MeatEatersUpgradeVision)
        {
            // Set Panel to active.
            if (storyTellingPanel.activeInHierarchy == false) storyTellingPanel.SetActive(true);

            // Hide Buttons.
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);

            PauseGame();

            UpdatePanel("Meat-Eaters Upgrade: Vision Increased\n\nCurrent Speed Level: " + GameObject.Find("Game").GetComponent<GameMain>().meatEaterSpeedLevel.ToString()
                + "\n\nCurrent Vision Level: " + GameObject.Find("Game").GetComponent<GameMain>().MeatEaterVisionDistanceLevel.ToString());

            if (gotIt)
            {
                gotIt = false;
                MeatEatersUpgradeVision = false;
                UnhideButtons();
                UnPauseGame();
                ExitPanel();
            }
        }

    }

    public void ClickYes()
    {
        yes = true;
    }

    public void ClickNo()
    {
        no = true;
    }

    public void ClickGotIt()
    {
        gotIt = true;
    }

    public void ExitPanel()
    {
        storyTellingPanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameObject.Find("Game").GetComponent<GameMain>().gamePaused = true;
    }

    public void UnPauseGame()
    {
        GameObject.Find("Game").GetComponent<GameMain>().gamePaused = false;
    }

    public void UpdatePanel(string story)
    {
        storyText.text = story;
    }

    public void UnhideButtons()
    {
        if (yesButton.isActiveAndEnabled == false) yesButton.gameObject.SetActive(true);
        if (noButton.isActiveAndEnabled == false) noButton.gameObject.SetActive(true);
        if (gotItButton.isActiveAndEnabled == false) gotItButton.gameObject.SetActive(true);
    }
}
