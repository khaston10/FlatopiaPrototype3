﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    #region Variables

    #region Plant Eater Variables
    public int plantEaters;
    public float plantEaterSpeed = .5f; // Set to .5 in normal speed and 2 for Fast Forward mode.
    public float caffeinePlantEaterSpeed = 1.5f; // Set to 1.5 in normal speed and 6 for Fast Forward mode.
    public int visionDistance = 5;
    public int MeatEaterVisionDistanceLevel = 1; // This should max out at 5.
    public List<Transform> plantEaterList;
    public Transform plantEater;
    public List<Vector3> plantEaterStartPositions;
    public int noPlantEatersKilledForXDays = 0;
    public Animation anim;
    #endregion

    #region Meat Eater Variables
    public int meatEaters;
    public int daysBetweenMeatEaterSpawn;
    public int meatEaterSpawnAmount = 2;
    public int daysUntilMeatEaterStarves;
    public int meaterEaterSpawnCounter = 0;
    public bool meatEatersSpawnDisabled = false; // This is used to temporaly disable meat eater spawn when user kills meat eaters.
    public float meatEaterSpeed = .5f; // Set to .5 in normal speed and 2 for Fast Forward mode. Then upped by .5 for each level.
    public int meatEaterSpeedLevel = 1; // This should max out at 5.
    private int daysUntilMeatEaterCounter;
    public Transform meatEater;
    private int randomMeatEaterUpgrade = 0;
    public List<Transform> meatEaterList;
    #endregion

    #region Food Variables
    public Transform food;
    public List<Vector3> foodPositions;
    public List<Transform> foodList;
    #endregion

    #region Upgrade Variables
    public bool slayerUpgradeUnlocked = false;
    public bool freezeUpgradeUnlocked = false;
    public bool feedUpgradeUnlocked = false;
    public bool caffeineUpgradeUnlocked = false;

    public bool caffeineSpeedOn = false;
    public bool meatEatersFrozen = false;

    // This array will be the buttons the user clicks to use upgrades. index = 0 will represent slot 1 and index = 1: slots 2...
    public Button[] UGSlots;

    public RawImage UGSlot1Image;
    public RawImage UGSlot2Image;
    public RawImage UGSlot3Image;
    public RawImage UGSlot4Image;

    public Texture[] UGTextures;

    private float slayerTimer;
    private float freezeTimer;
    private float feedTimer;
    private float caffeineTimer;

    private int slayerTimerReset = 10;
    private int freezeTimerReset = 10;
    private int feedTimerReset = 10;
    private int caffeineTimerReset = 10;

    // This array will repsent which UG slot is holding which upgrade. The first int in the array represents slot 1. A value of 0 means Slayer.
    // int 4 means not active, 0: slayer, 1: freeze, 2: feed, 3: caffeine. So an array of [0, 3, 3, 4] would mean Slot 1 has slayer, 2 and 3 have
    // caffeine and 4 has not been assigned.
    public int[] UGSlotsActive;

    // These bools are used when the slot has a upgrade in it but is not available for use. i.e. between uses
    public bool UGSlayerIsAvailable = true;
    public bool UGFreezeIsAvailable;
    public bool UGFeedIsAvailable;
    public bool UGCaffeineIsAvailable;


    #endregion

    #region Acheivment Variables
    public RawImage landOwnerImage;
    public Texture landOwnerTexture;
    public bool landOwnerAchievement;
    public RawImage genocideImage;
    public Texture genocideTexture;
    public bool genocideAchievement;
    public RawImage ninjaImage;
    public Texture ninjaTexture;
    public bool ninjaAchievement;
    public RawImage survivalistImage;
    public Texture survivalistTexture;
    public bool survivalistAchievement;
    public RawImage glutonImage;
    public Texture glutonTexture;
    public bool glutonAchievement;
    public RawImage unlockedImage;
    public Texture unlockedTexture;
    public bool unlockedAchievement;
    #endregion

    #region Region Variables

    // This is an array of buttons - 0: Region1 (Forest), 1: Region2 (Desert), 2: Region3 (Jungle), 3: Region4 (Tundra)
    public Button[] regionButtons;

    // This is an array of booleans with information about which region is active.
    public bool[] regionIsActive;

    public int region2WorldSize;

    #endregion

    #region Main Variables
    public bool gameWon;
    public int worldSize;
    public int foodSpawned;
    public int gamePoints;
    public bool creaturesAwake = true;
    public bool gamePaused = false;
    public float slowSpeedMeter = .0005f; // Set to .0005 in normal speed and .002 for Fast Forward mode.
    public float medSpeedMeter = .001f; // Set to .001 in normal speed and .004 for Fast Forward mode.
    public Light star1;
    public Transform planet1;
    public Animator planetAnimator;
    public Transform star1Body;
    public Animator starAnimator;
    private int worldSizeLimit = 30;
    public int researchPoints = 0;
    public int PeakPop = 0;
    //------------------------------------This code is used for debugging, remove before release.--------------------
    public bool foodOn = true;
    public bool movePlanetOn = true;
    public bool moveStarOn = true;
    public bool plantEatersOn = true;
    public bool meathEatersOn = true;

    public int day;
    private float timer = 0.0f;
    private float plantEaterTimer = 0.0f;
    private float lengthOfDay = 25.12f;
    private float timerSpeedCoefficient = .25f; // This value depends on the lengthOfDay. If Day is 360 the coefficient should be 1.
    // If lengthOfDay is 2 *360, or 720 the coefficient should be 1/2 or .5, and so on. 
    private int gameSpeed = 1;
    private float yPos;
    private float xPos;
    private float star1YPos;
    private float star1XPos;
    private float planetYPos;
    private float planetZPos;
    private float planetXPos;
    public GameObject settingsPanel;

    // Buttons from Control Panel
    public Button foodSpawnButton;
    public Button worldSizeButton;
    public Button plantEaterButton;
    #endregion

    #region Text Variables
    public Text GameSpeedDisplayText;
    public Text gamePointsText;
    public Text worldSizeText;
    public Text foodSpawnedText;
    public Text PlantEatersText;
    public Text dayCountText;
    public Text slayerSliderText;
    public Text freezeSliderText;
    public Text feedSliderText;
    public Text caffeineSliderText;
    public Text daysUntilMeatEatersText;
    public Text researchPointsText;
    public Text peakPopText;

    #endregion

    #region Outposts
    // Bools to keep track of advanced upgrades. Bools that are not being used yet have been commented out.
    public bool s1Unlocked = false;
    public bool s2Unlocked = false;
    //private bool s3Unlocked = false;
    //private bool s4Unlocked = false;
    //private bool s5Unlocked = false;
    public bool m1Unlocked = false;
    public bool m2Unlocked = false;
    //private bool m3Unlocked = false;
    //private bool m4Unlocked = false;
    //private bool m5Unlocked = false;
    #endregion

    #region Audio Variables
    // Sounds to play depending on state of game.
    public AudioClip buttonDoesNotWork;
    public AudioClip upgradeUnlocked;
    public AudioClip plantEaterDie;
    public AudioClip meatEaterDie;
    public AudioClip achievementUnlocked;
    #endregion

    #region Color Variables
    private Color starvingPlantEater = new Color(.8f, .7f, .3f, .1f);
    private Color starvingMeatEater = new Color(.8f, .7f, .3f, .1f);
    private Color frozenMeatEater = new Color(.1f, .7f, .1f, .1f);
    private Color FedPlantEater = new Color(.1f, 1f, .1f, 1f);
    private Color CafeinePlantEater = new Color(1f, .1f, .1f, 1f);
    private Color GreyTrans = new Color(.3f, .3f, .3f, .3f);
    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Load Data
        worldSize = GlobalControl.Instance.worldSize;
        foodSpawned = GlobalControl.Instance.foodSpawned;
        plantEaters = GlobalControl.Instance.plantEaters;
        gamePoints = GlobalControl.Instance.gamePoints;
        gameWon = GlobalControl.Instance.gameWon;
        day = GlobalControl.Instance.day;
        landOwnerAchievement = GlobalControl.Instance.landOwnerAchievement;
        genocideAchievement = GlobalControl.Instance.genocideAchievement;
        ninjaAchievement = GlobalControl.Instance.ninjaAchievement;
        survivalistAchievement = GlobalControl.Instance.survivalistAchievement;
        glutonAchievement = GlobalControl.Instance.glutonAchievement;
        unlockedAchievement = GlobalControl.Instance.unlockedAchievement;
        #endregion

        #region Set Objects Active/Inactive
        daysUntilMeatEaterCounter = daysBetweenMeatEaterSpawn;
        daysUntilMeatEatersText.text = daysUntilMeatEaterCounter.ToString() + " Days";
        daysUntilMeatEaterCounter -= 1;

        UGSlot1Image.enabled = false;
        UGSlot2Image.enabled = false;
        UGSlot3Image.enabled = false;
        UGSlot4Image.enabled = false;

        #endregion

        #region Populate World
        // Populate the positions list with all possible positions for grids.
        for (int i = 0; i < worldSize; i++)
        {
            for (int j = 0; j < worldSize; j++)
            {
                Vector3 foodPos = new Vector3(i, 1f, j);
                Vector3 plantEaterPos = new Vector3(i, 1.2f, j);
                foodPositions.Add(foodPos);
                plantEaterStartPositions.Add(plantEaterPos);
            }
        }

        // Stock food at the beginning of day 1.
        for (int i = 0; i < foodSpawned; i++)
        {
            // Pick a random position.
            int randPos = Random.Range(0, foodPositions.Count);

            // Place food at position and remove its position from the available list, then place the transform in the foodList. 
            Transform f = Instantiate(food);
            f.localPosition = foodPositions[randPos];
            foodPositions.RemoveAt(randPos);
            foodList.Add(f);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        // Start timer for day, when the timer reaches length_of_day the timer reasets, if game is paused then do not update time.
        if (gamePaused != true)
        {
            timer += Time.deltaTime;
            plantEaterTimer += Time.deltaTime;
            
        }

        #region Get User Input
        // Get Keyboard Inputs.
        if (Input.GetKeyDown(KeyCode.Alpha1)) foodSpawnButton.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha2)) worldSizeButton.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha3)) plantEaterButton.onClick.Invoke();
        #endregion

        #region Update Objects
        //------------------------------------This code is used for debugging, remove before release.--------------------
        if (foodOn == false)
        {
            for (int i = 0; i < foodList.Count; i++)
            {
                foodPositions.Add(foodList[i].localPosition);
                Destroy(foodList[i].gameObject);
            }
            foodList.Clear();
        }

        // Restock plant eaters at the beginning of each day.
        if (plantEaterList.Count < plantEaters && plantEatersOn && creaturesAwake)
        {
            // Pick a random position.
            int randPos = Random.Range(0, plantEaterStartPositions.Count);

            // Place plant eater at position, then place the transform in the plantEaterList. 
            Transform p = Instantiate(plantEater);
            p.localPosition = plantEaterStartPositions[randPos];
            plantEaterList.Add(p);

        }

        // Restock meat eaters at the beginning of the day if is time, then reset the counter.
        if (meaterEaterSpawnCounter == daysBetweenMeatEaterSpawn && meatEatersSpawnDisabled == false)
        {
            // Pick a random upgrade.
            if (day > 6)
            {
                randomMeatEaterUpgrade = Random.Range(0, 3);
            }
            
            if (randomMeatEaterUpgrade == 1 && meatEaterSpeedLevel < 5)
            {
                // Upgrade Speed.
                meatEaterSpeedLevel += 1;
                GameObject.Find("Canvas").GetComponent<StoryTeller>().MeatEatersUpgradeSpeed = true;
            }

            else if (randomMeatEaterUpgrade == 2 && MeatEaterVisionDistanceLevel < 5)
            {
                // Upgrade Vision.
                MeatEaterVisionDistanceLevel += 1;
                visionDistance += 3;
                GameObject.Find("Canvas").GetComponent<StoryTeller>().MeatEatersUpgradeVision = true;
            }

            for (int i = 0; i < meatEaterSpawnAmount; i++)
            {
                // Pick a random position.
                int randPos = Random.Range(0, plantEaterStartPositions.Count);

                // Place meat eater at position, then place the transform in the meatEaterList. 
                Transform m = Instantiate(meatEater);
                m.localPosition = plantEaterStartPositions[randPos];
                meatEaterList.Add(m);

                meatEaters += 1;

            }
            meatEatersSpawnDisabled = true;
            meatEaterSpawnAmount += 1;
        }

        else if (meaterEaterSpawnCounter > daysBetweenMeatEaterSpawn)
        {
            meaterEaterSpawnCounter = 0;
            meatEatersSpawnDisabled = false;
        }

            //------------------------------------This code is used for debugging, remove before release.--------------------
            if (plantEatersOn == false)
        {
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                plantEaterStartPositions.Add(plantEaterList[i].localPosition);
                Destroy(plantEaterList[i].gameObject);
            }
            plantEaterList.Clear();
        }


        // Update panels.
        GameSpeedDisplayText.text = gameSpeed.ToString();
        gamePointsText.text = gamePoints.ToString();
        worldSizeText.text = worldSize.ToString();
        foodSpawnedText.text = foodSpawned.ToString();
        PlantEatersText.text = plantEaters.ToString();
        dayCountText.text = day.ToString();

        // Update Upgrade Timers and set upgrades avaliable.
        if (UGSlayerIsAvailable == false && slayerTimer > slayerTimerReset)
        {
            UGSlayerIsAvailable = true;
            // Find which slot it is in so the button's color can be changed.
            ChangeButtonColor(UGSlots[System.Array.IndexOf(UGSlotsActive, 0)], Color.white);
            AudioSource.PlayClipAtPoint(upgradeUnlocked, transform.position);
        }

        if (UGFreezeIsAvailable == false && freezeTimer > freezeTimerReset)
        {
            UGFreezeIsAvailable = true;
            // Find which slot it is in so the button's color can be changed.
            ChangeButtonColor(UGSlots[System.Array.IndexOf(UGSlotsActive, 1)], Color.white);
            AudioSource.PlayClipAtPoint(upgradeUnlocked, transform.position);
        }

        if (UGFeedIsAvailable == false && feedTimer > feedTimerReset)
        {
            UGFeedIsAvailable = true;
            // Find which slot it is in so the button's color can be changed.
            ChangeButtonColor(UGSlots[System.Array.IndexOf(UGSlotsActive, 2)], Color.white);
            AudioSource.PlayClipAtPoint(upgradeUnlocked, transform.position);
        }

        if (UGCaffeineIsAvailable == false && caffeineTimer > caffeineTimerReset)
        {
            UGCaffeineIsAvailable = true;
            // Find which slot it is in so the button's color can be changed.
            ChangeButtonColor(UGSlots[System.Array.IndexOf(UGSlotsActive, 3)], Color.white);
            AudioSource.PlayClipAtPoint(upgradeUnlocked, transform.position);
        }
        slayerTimer += Time.deltaTime;
        freezeTimer += Time.deltaTime;
        feedTimer += Time.deltaTime;
        caffeineTimer += Time.deltaTime;

        // Check to see if creatures should be awake or asleep.
        if (timer >= (lengthOfDay / 2) && creaturesAwake == true)
        {
            creaturesAwake = false;

            // Play the Idle Animation for plant eaters.
            PlayPlanetEaterAnimation("idle");

            // Play the Idle animation for meat eaters.
            PlayMeatEaterAnimation("wait");
        }

        else if (timer < (lengthOfDay / 2) && creaturesAwake == false)
        {
            creaturesAwake = true;

            // Play the Walk Animation for plant eaters.
            PlayPlanetEaterAnimation("walk");

            // Play the Walk animation for meat eaters.
            PlayMeatEaterAnimation("walk");
        }

        // Update planet positions.
        //------------------------------------This code is used for debugging, remove before release.--------------------
        if (moveStarOn)
        {
            star1YPos = worldSize * Mathf.Sin(timerSpeedCoefficient * timer);
            star1XPos = worldSize * Mathf.Cos(timerSpeedCoefficient * timer) + worldSize / 2;
            star1Body.position = new Vector3(xPos, yPos, worldSize / 2);
        }

        // Update Star animation when game is paused.
        if (starAnimator.GetCurrentAnimatorStateInfo(0).IsName("SpinStar1") && gamePaused)
        {
            starAnimator.Play("StarPaused");
        }

        else if (starAnimator.GetCurrentAnimatorStateInfo(0).IsName("StarPaused") && gamePaused == false)
        {
            starAnimator.Play("SpinStar1");
        }



        //------------------------------------This code is used for debugging, remove before release.--------------------
        if (movePlanetOn)
        {
            planetZPos = worldSize * -3 * Mathf.Cos(timerSpeedCoefficient * timer);
            planetYPos = worldSize * Mathf.Cos(timerSpeedCoefficient * timer);
            planetXPos = worldSize * Mathf.Sin(timerSpeedCoefficient * timer) + worldSize / 2;
            planet1.position = new Vector3(planetXPos, planetYPos, planetZPos);
        }

        // Update Planet animation when game is paused.
        if (planetAnimator.GetCurrentAnimatorStateInfo(0).IsName("StarSpin") && gamePaused)
        {
            planetAnimator.Play("PlanetPause");
        }

        else if (planetAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlanetPause") && gamePaused == false)
        {
            planetAnimator.Play("StarSpin");
        }



            // Move the source of light, star1.
            yPos = worldSize * Mathf.Sin(timerSpeedCoefficient * timer);
        xPos = worldSize * Mathf.Cos(timerSpeedCoefficient * timer) + worldSize / 2;
        star1.transform.position = new Vector3(xPos, yPos, worldSize / 2);
        #endregion

        #region Take Care of End Of Day
        // Take care of end of day tasks.
        if (timer >= lengthOfDay)
        {
            
            // Restock food at the beginning of each day.
            int currentFoodAmount = foodList.Count;
            for (int i = currentFoodAmount; i < foodSpawned; i++ )
            {
                // Pick a random position.
                int randPos = Random.Range(0, foodPositions.Count);

                // Place food at position and remove its position from the available list, then place the transform in the foodList. 
                Transform f = Instantiate(food);
                f.localPosition = foodPositions[randPos];
                foodPositions.RemoveAt(randPos);
                foodList.Add(f);
            }

            // Increase the NoPlantEatersKilled by 1. This is used for the Ninja achievement.
            noPlantEatersKilledForXDays += 1;

            // Check through the list of plant eaters and destroy plant eaters that did not survive.
            for (int i = plantEaterList.Count - 1; i >= 0; i--)
            {
                if (plantEaterList[i].GetComponent<PlantEaterContoller>().foodEaten == 0)
                {
                    AudioSource.PlayClipAtPoint(plantEaterDie, transform.position);

                    Destroy(plantEaterList[i].gameObject);
                    plantEaterList.RemoveAt(i);
                    plantEaters -= 1;
                }

                // Check to see if the Gluton Achievement has been unlocked.
                else if (plantEaterList[i].GetComponent<PlantEaterContoller>().foodEaten >= 5 && glutonAchievement == false)
                {
                    AudioSource.PlayClipAtPoint(achievementUnlocked, transform.position);
                    glutonImage.color = Color.white;
                    glutonImage.texture = glutonTexture;
                    glutonAchievement = true;
                }
            }

            // Check through the list of meat eaters and destroy meat eaters that did not survive.
            for (int i = meatEaterList.Count - 1; i >= 0; i--)
            {
                if (meatEaterList[i].GetComponent<MeatEaterContoller>().daysSinceLastEaten >= daysUntilMeatEaterStarves)
                {
                    AudioSource.PlayClipAtPoint(meatEaterDie, transform.position);
                    Destroy(meatEaterList[i].gameObject);
                    meatEaterList.RemoveAt(i);
                    meatEaters -= 1;
                }

            }

            // Reset food Eaten for plant eaters.
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                plantEaterList[i].GetComponent<PlantEaterContoller>().foodEaten = 0;
            }

            // Add 1 days to meat eaters starve counter.
            for (int i = 0; i < meatEaterList.Count; i++)
            {
                meatEaterList[i].GetComponent<MeatEaterContoller>().daysSinceLastEaten += 1;
            }

            // Check to see if the Ninja Achievement has been unlocked.
            if (noPlantEatersKilledForXDays == 10 && ninjaAchievement == false)
            {
                AudioSource.PlayClipAtPoint(achievementUnlocked, transform.position);
                ninjaImage.color = Color.white;
                ninjaImage.texture = ninjaTexture;
                ninjaAchievement = true;
            }

            // Check to see if the Survivalist Achievement has been unlocked.
            if (day == 40 && survivalistAchievement == false)
            {
                AudioSource.PlayClipAtPoint(achievementUnlocked, transform.position);
                survivalistImage.color = Color.white;
                survivalistImage.texture = survivalistTexture;
                survivalistAchievement = true;
            }

            // Update the daysUntilMeatEater text updated.
            daysUntilMeatEatersText.text = daysUntilMeatEaterCounter.ToString() + " Days";
            if (daysUntilMeatEaterCounter == 0) daysUntilMeatEaterCounter = daysBetweenMeatEaterSpawn;
            else daysUntilMeatEaterCounter -= 1;

            timer = 0;
            day += 1;
            meaterEaterSpawnCounter += 1;
            meatEatersSpawnDisabled = false;
            meatEatersFrozen = false;

            // Turn Caffeine Speed off.
            caffeineSpeedOn = false;

            // At the begining of the day. Check to see if plant eaters and meat eaters have beeen fed.
            //If they have not, change their skin color to the starving color.
            // Change the color of creatures skin if they are starving and going to die.
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                if (plantEaterList[i].GetComponent<PlantEaterContoller>().foodEaten == 0)
                {
                    plantEaterList[i].GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = starvingPlantEater;
                }

            }

            for (int i = 0; i < meatEaterList.Count; i++)
            {
                if (meatEaterList[i].GetComponent<MeatEaterContoller>().daysSinceLastEaten == daysUntilMeatEaterStarves)
                {
                    meatEaterList[i].GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = starvingMeatEater;
                    meatEaterList[i].GetChild(0).GetChild(2).GetComponent<Renderer>().material.color = starvingMeatEater;
                    meatEaterList[i].GetChild(0).GetChild(3).GetComponent<Renderer>().material.color = starvingMeatEater;
                }
            }

            // If the user is using the tutorial. Open tutorial panel.
            if(day == 2 && GameObject.Find("Canvas").GetComponent<StoryTeller>().tutorialOn)
            {
                GameObject.Find("Canvas").GetComponent<StoryTeller>().askUserToFindTheDay = true;
            }

            else if (day == 3 && GameObject.Find("Canvas").GetComponent<StoryTeller>().tutorialOn)
            {
                GameObject.Find("Canvas").GetComponent<StoryTeller>().askUserToFindGamePoints = true;
            }

            else if (day == 4 && GameObject.Find("Canvas").GetComponent<StoryTeller>().tutorialOn)
            {
                GameObject.Find("Canvas").GetComponent<StoryTeller>().askUserToUseSettings = true;
            }

            else if (day == 5 && GameObject.Find("Canvas").GetComponent<StoryTeller>().tutorialOn)
            {
                GameObject.Find("Canvas").GetComponent<StoryTeller>().askUserToLearnAboutMeat = true;
            }

            else if (day == 6 && GameObject.Find("Canvas").GetComponent<StoryTeller>().tutorialOn)
            {
                GameObject.Find("Canvas").GetComponent<StoryTeller>().endTutorial = true;
            }

            // Check to see if the game is over.
            if (plantEaters == 0)
            {
                SaveData();
                SceneManager.LoadScene(sceneName: "EndScreen");
            }

            // Check to see if the game has been won.
            if (ninjaAchievement && genocideAchievement && glutonAchievement && survivalistAchievement && landOwnerAchievement && unlockedAchievement && s2Unlocked && m2Unlocked)
            {
                gameWon = true;
                SaveData();
                SceneManager.LoadScene(sceneName: "EndScreen");
            }

        }
        #endregion
    }

    #region Functions
    // Save data to Global Control
    public void SaveData()
    {
        GlobalControl.Instance.worldSize = worldSize;
        GlobalControl.Instance.foodSpawned = foodSpawned;
        GlobalControl.Instance.plantEaters = plantEaters;
        GlobalControl.Instance.gamePoints = gamePoints;
        GlobalControl.Instance.gameWon = gameWon; ;
        GlobalControl.Instance.day = day;
        GlobalControl.Instance.peakPop = PeakPop;
        GlobalControl.Instance.landOwnerAchievement = landOwnerAchievement;
        GlobalControl.Instance.ninjaAchievement = ninjaAchievement;
        GlobalControl.Instance.genocideAchievement = genocideAchievement;
        GlobalControl.Instance.glutonAchievement = glutonAchievement;
        GlobalControl.Instance.unlockedAchievement = unlockedAchievement;
        GlobalControl.Instance.survivalistAchievement = survivalistAchievement;
        
    }

    // This section handles buttons on screen.
    public void ClickPause()
    {
        if (gamePaused == false)
        {
            gamePaused = true;
            gameSpeed = 0;
        }

        // if there is Science Outpost this will pasue the outpost so Research points and not being earned.
        if (s1Unlocked && s2Unlocked == false)
        {
            GameObject.Find("ScienceOutpost(Clone)").GetComponent<ScienceOutpost>().paused = true;
        }

        else if (s2Unlocked)
        {
            GameObject.Find("ScienceOutpost2(Clone)").GetComponent<ScienceOutpost>().paused = true;
        }
    }

    public void ClickForward()
    {
        // Only makes changes if the lengthOfDay = 360, meaning forward mode was occuring. 
        if (timerSpeedCoefficient == 1)
        {
            lengthOfDay = 25.12f;
            timerSpeedCoefficient = .25f;
            timer *= 4;
            gameSpeed = 1;
        }

        // only makes changes when this button is pushed while game is paused.
        if (gamePaused == true)
        {
            gamePaused = false;
        }

        // Update the plant eater's and meat eater's speed to match fast forward.
        plantEaterSpeed = .5f;
        caffeinePlantEaterSpeed = 1.5f;
        meatEaterSpeed = .25f * meatEaterSpeedLevel + .25f;
        slowSpeedMeter = .0005f;
        medSpeedMeter = .001f;

        // Update the projectile speed if there is a military.
        if (m1Unlocked && m2Unlocked ==false)
        {
            GameObject.Find("MilitaryOutpost(Clone)").GetComponent<MilitaryOutpost>().projectileSpeed = 30;
        }

        else if (m2Unlocked)
        {
            GameObject.Find("MilitaryOutpost2(Clone)").GetComponent<MilitaryOutpost>().projectileSpeed = 30;
        }

        // if there is Science Outpost this will un-pause the outpost so Research points and not being earned.
        if (s1Unlocked && s2Unlocked == false)
        {
            GameObject.Find("ScienceOutpost(Clone)").GetComponent<ScienceOutpost>().paused = false;
            GameObject.Find("ScienceOutpost(Clone)").GetComponent<ScienceOutpost>().researchPointsSpeed = 10;
        }

        else if (s2Unlocked)
        {
            GameObject.Find("ScienceOutpost2(Clone)").GetComponent<ScienceOutpost>().paused = false;
            GameObject.Find("ScienceOutpost2(Clone)").GetComponent<ScienceOutpost>().researchPointsSpeed = 4;
        }
    }

    public void ClickFastForward()
    {
        // Only make changes if the lengthOFDay = 1440, meaning fast forward was occuring. 
        if (timerSpeedCoefficient == .25)
        {
            lengthOfDay = 6.28f;
            timerSpeedCoefficient = 1f;
            timer /= 4;
            gameSpeed = 4;
        }

        // only makes changes when this button is pushed while game is paused.
        if (gamePaused == true)
        {
            gamePaused = false;
        }

        // Update the plant eater's and meat eater's speed to match fast forward.
        plantEaterSpeed = 2f;
        caffeinePlantEaterSpeed = 6f;
        meatEaterSpeed = meatEaterSpeedLevel + 1;
        slowSpeedMeter = .002f;
        medSpeedMeter = .004f;

        //Update projectile speed if there is a military.
        if (m1Unlocked && m2Unlocked ==false)
        {
            GameObject.Find("MilitaryOutpost(Clone)").GetComponent<MilitaryOutpost>().projectileSpeed = 120;
        }

        else if (m2Unlocked)
        {
            GameObject.Find("MilitaryOutpost2(Clone)").GetComponent<MilitaryOutpost>().projectileSpeed = 120;
        }

        // if there is Science Outpost this will un-pause the outpost so Research points and not being earned.
        if (s1Unlocked && s2Unlocked == false)
        {
            GameObject.Find("ScienceOutpost(Clone)").GetComponent<ScienceOutpost>().paused = false;
            //This is not the correct value but it is close. 10 divided by 4.
            GameObject.Find("ScienceOutpost(Clone)").GetComponent<ScienceOutpost>().researchPointsSpeed = 2;
        }

        else if (s2Unlocked)
        {
            GameObject.Find("ScienceOutpost2(Clone)").GetComponent<ScienceOutpost>().paused = false;
            GameObject.Find("ScienceOutpost2(Clone)").GetComponent<ScienceOutpost>().researchPointsSpeed = 1;
        }

    }

    public void ClickFoodSpawned()
    {
        if (gamePoints > 0 && foodPositions.Count > 0 && foodSpawned < worldSize * worldSize)
        {
            gamePoints -= 1;
            foodSpawned += 1;
        }
    }

    public void ClickWorldSize()
    {
        if (gamePoints > 0 && worldSize < worldSizeLimit && GameObject.Find("Grid").GetComponent<CreateGrid>().cameras[0].enabled == true)
        {
            gamePoints -= 1;  
            worldSize += 1;
            GameObject.Find("Grid").GetComponent<CreateGrid>().UpdateGrid();
            GameObject.Find("Grid").GetComponent<CreateGrid>().UpdateCameraPos(0);
        }

        else if (gamePoints > 0 && region2WorldSize < worldSizeLimit && GameObject.Find("Grid").GetComponent<CreateGrid>().cameras[1].enabled == true)
        {
            gamePoints -= 1;
            region2WorldSize += 1;
            GameObject.Find("Grid").GetComponent<CreateGrid>().UpdateGridRegion2();
            GameObject.Find("Grid").GetComponent<CreateGrid>().UpdateCameraPos(1);
        }

        // Check to see if the Land Owner achievement is unlocked.
        if (worldSize >= 30 && landOwnerAchievement == false)
        {
            AudioSource.PlayClipAtPoint(achievementUnlocked, transform.position);
            landOwnerImage.color = Color.white;
            landOwnerImage.texture = landOwnerTexture;
            landOwnerAchievement = true;
        }
            

    }

    public void ClickPlantEaters()
    {
        if (gamePoints > 0)
        {
            gamePoints -= 1;
            plantEaters += 1;

            // Check to see if new Peak Population has been reached.
            if (plantEaters > PeakPop)
            {
                PeakPop = plantEaters;
                peakPopText.text = PeakPop.ToString();
            }
        }
    }

    public void ClickSlayerUpgrade()
    {
        if (slayerUpgradeUnlocked && slayerTimer > slayerTimerReset)
        {

            //AudioSource.PlayClipAtPoint(upgradeUnlocked, transform.position);
            AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);

            // Check to see if genocide achievement is unlocked.
            if (meatEaterList.Count >= 5)
            {
                AudioSource.PlayClipAtPoint(achievementUnlocked, transform.position);
                genocideImage.color = Color.white;
                genocideImage.texture = genocideTexture;
                genocideAchievement = true;
            }

            // Kill meat eaters.
            for (int i = 0; i < meatEaterList.Count; i++)
            {
                Destroy(meatEaterList[i].gameObject);
            }
            meatEaterList.Clear();


            // Set the bool to false so that meat eaters do not come back immediately.
            meatEatersSpawnDisabled = true;

            // reset timer.
            slayerTimer = 0;

        }

        else AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);

    }

    public void ClickFreezeUpgrade()
    {
        if (freezeUpgradeUnlocked && creaturesAwake && freezeTimer > freezeTimerReset)
        {

            AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);
            // Set the freeze bool for meat eaters.
            meatEatersFrozen = true;

            ChangeMeatEaterSkin(frozenMeatEater);

            //Reset Timer.
            freezeTimer = 0;
        }

        else AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);
    }

    public void ClickFeedUpgrade()
    {
        if (feedUpgradeUnlocked && feedTimer > feedTimerReset)
        {
            AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);


            // Feed all plant eaters and change their skin color.
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                plantEaterList[i].GetComponent<PlantEaterContoller>().foodEaten += 1;
                plantEaterList[i].GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedPlantEater;
            }

            //Reset Timer.
            feedTimer = 0;

        }

        else AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);

    }

    public void ClickCaffeineUpgrade()
    {
        if (caffeineUpgradeUnlocked && caffeineTimer > caffeineTimerReset)
        {
            AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);

            // Set caffeine speed on;
            caffeineSpeedOn = true;

            // Change the skin color of the plant Eaters.
            ChangePlantEaterSkin(CafeinePlantEater);

            // Reset Timer.
            caffeineTimer = 0;
        }

        else AudioSource.PlayClipAtPoint(buttonDoesNotWork, Vector3.zero);

    }

    public void ChangeButtonColor(Button but, Color color)
    {
        but.GetComponent<Image>().color = color;
    }

    public void UpdateResearchPoints()
    {
        if (s1Unlocked)
        {
            researchPointsText.text = researchPoints.ToString();

        }
    }

    public void ChangePlantEaterSkin(Color color)
    {
        for (int i = 0; i < plantEaterList.Count; i++)
        {
            plantEaterList[i].GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = color;
        }
    }

    public void ChangeMeatEaterSkin(Color color)
    {
        for (int i = 0; i < meatEaterList.Count; i++)
        {
            meatEaterList[i].GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = color;
        }
    }

    public void PlayPlanetEaterAnimation(string animationName)
    {
        if (animationName == "idle")
        {
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                plantEaterList[i].GetComponent<PlantEaterContoller>().isAwake = false;
                plantEaterList[i].GetChild(0).GetComponent<Animation>().Play("idle");
            }
        }

        else if (animationName == "walk")
        {
            for (int i = 0; i < plantEaterList.Count; i++)
            {
                anim = plantEaterList[i].GetChild(0).GetComponent<Animation>();
                anim[animationName].speed = 2;
                plantEaterList[i].GetComponent<PlantEaterContoller>().isAwake = true;
                plantEaterList[i].GetChild(0).GetComponent<Animation>().Play("walk");
            }
        }
        
    }

    public void PlayMeatEaterAnimation(string animationName)
    {
        if (animationName == "wait")
        {
            for (int i = 0; i < meatEaterList.Count; i++)
            {
                meatEaterList[i].GetComponent<MeatEaterContoller>().isAwake = false;
                meatEaterList[i].GetChild(0).GetComponent<Animation>().Play("wait");
            }
        }

        else if (animationName == "walk")
        {
            for (int i = 0; i < meatEaterList.Count; i++)
            {
                meatEaterList[i].GetComponent<MeatEaterContoller>().isAwake = true;
                meatEaterList[i].GetChild(0).GetComponent<Animation>().Play("walk");
            }
        }
    }

    public void UpgradesUpdated(int upgrade)
    {
        // Check to see if the upgrade is already in a slot.
        for (int i = 0; i < UGSlotsActive.Length; i++)
        {
            if (upgrade == UGSlotsActive[i]) return;
        }

        // Check to see if there is an open slot.
        if(UGSlotsActive[0] == 4)
        {
            UGSlotsActive[0] = upgrade;
            UGSlot1Image.enabled = true;
            UGSlot1Image.texture = UGTextures[upgrade];
        }

        else if (UGSlotsActive[1] == 4)
        {
            UGSlotsActive[1] = upgrade;
            UGSlot2Image.enabled = true;
            UGSlot2Image.texture = UGTextures[upgrade];
        }

        else if (UGSlotsActive[2] == 4)
        {
            UGSlotsActive[2] = upgrade;
            UGSlot3Image.enabled = true;
            UGSlot3Image.texture = UGTextures[upgrade];
        }

        else if (UGSlotsActive[3] == 4)
        {
            UGSlotsActive[3] = upgrade;
            UGSlot4Image.enabled = true;
            UGSlot4Image.texture = UGTextures[upgrade];
        }
    }

    public void ClickUpgrade(int slot)
    {
        if (slot == 1)
        {
            if(UGSlotsActive[0] == 0 && UGSlayerIsAvailable)
            {
                ClickSlayerUpgrade();
                UGSlayerIsAvailable = false;
                slayerTimer = 0;
                ChangeButtonColor(UGSlots[0], GreyTrans);
            }
            else if(UGSlotsActive[0] == 1 && UGFreezeIsAvailable && creaturesAwake)
            {
                ClickFreezeUpgrade();
                UGFreezeIsAvailable = false;
                freezeTimer = 0;
                ChangeButtonColor(UGSlots[0], GreyTrans);
            }
            else if(UGSlotsActive[0] == 2 && UGFeedIsAvailable && creaturesAwake)
            {
                ClickFeedUpgrade();
                UGFeedIsAvailable = false;
                feedTimer = 0;
                ChangeButtonColor(UGSlots[0], GreyTrans);
            }
            else if (UGSlotsActive[0] == 3 && UGCaffeineIsAvailable && creaturesAwake)
            {
                ClickCaffeineUpgrade();
                UGCaffeineIsAvailable = false;
                caffeineTimer = 0;
                ChangeButtonColor(UGSlots[0], GreyTrans);
            }
        }

        else if (slot == 2)
        {
            if (UGSlotsActive[1] == 0 && UGSlayerIsAvailable)
            {
                ClickSlayerUpgrade();
                UGSlayerIsAvailable = false;
                slayerTimer = 0;
                ChangeButtonColor(UGSlots[1], GreyTrans);
            }
            else if (UGSlotsActive[1] == 1 && UGFreezeIsAvailable && creaturesAwake)
            {
                ClickFreezeUpgrade();
                UGFreezeIsAvailable = false;
                freezeTimer = 0;
                ChangeButtonColor(UGSlots[1], GreyTrans);
            }
            else if (UGSlotsActive[1] == 2 && UGFeedIsAvailable && creaturesAwake)
            {
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                UGFeedIsAvailable = false;
                feedTimer = 0;
                ChangeButtonColor(UGSlots[1], GreyTrans);
            }
            else if (UGSlotsActive[1] == 3 && UGCaffeineIsAvailable && creaturesAwake)
            {
                ClickCaffeineUpgrade();
                UGCaffeineIsAvailable = false;
                caffeineTimer = 0;
                ChangeButtonColor(UGSlots[1], GreyTrans);
            }
        }

        else if (slot == 3)
        {
            if (UGSlotsActive[2] == 0 && UGSlayerIsAvailable)
            {
                ClickSlayerUpgrade();
                UGSlayerIsAvailable = false;
                slayerTimer = 0;
                ChangeButtonColor(UGSlots[2], GreyTrans);
            }
            else if (UGSlotsActive[2] == 1 && UGFreezeIsAvailable && creaturesAwake)
            {
                ClickFreezeUpgrade();
                UGFreezeIsAvailable = false;
                freezeTimer = 0;
                ChangeButtonColor(UGSlots[2], GreyTrans);
            }
            else if (UGSlotsActive[2] == 2 && UGFeedIsAvailable && creaturesAwake)
            {
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                UGFeedIsAvailable = false;
                feedTimer = 0;
                ChangeButtonColor(UGSlots[2], GreyTrans);
            }
            else if (UGSlotsActive[2] == 3 && UGCaffeineIsAvailable && creaturesAwake)
            {
                ClickCaffeineUpgrade();
                UGCaffeineIsAvailable = false;
                caffeineTimer = 0;
                ChangeButtonColor(UGSlots[2], GreyTrans);
            }
        }

        else if (slot == 4)
        {
            if (UGSlotsActive[3] == 0 && UGSlayerIsAvailable)
            {
                ClickSlayerUpgrade();
                UGSlayerIsAvailable = false;
                slayerTimer = 0;
                ChangeButtonColor(UGSlots[3], GreyTrans);
            }
            else if (UGSlotsActive[3] == 1 && UGFreezeIsAvailable && creaturesAwake)
            {
                ClickFreezeUpgrade();
                UGFreezeIsAvailable = false;
                freezeTimer = 0;
                ChangeButtonColor(UGSlots[3], GreyTrans);
            }
            else if (UGSlotsActive[3] == 2 && UGFeedIsAvailable && creaturesAwake)
            {
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                ClickFeedUpgrade();
                UGFeedIsAvailable = false;
                feedTimer = 0;
                ChangeButtonColor(UGSlots[3], GreyTrans);
            }
            else if (UGSlotsActive[3] == 3 && UGCaffeineIsAvailable && creaturesAwake)
            {
                ClickCaffeineUpgrade();
                UGCaffeineIsAvailable = false;
                caffeineTimer = 0;
                ChangeButtonColor(UGSlots[3], GreyTrans);
            }
        }
    }

    public void RegionUpdate(int regionNum)
    {
        if (regionIsActive[regionNum] == false)
        {
            regionIsActive[regionNum] = true;
            ChangeButtonColor(regionButtons[regionNum], Color.white);
        }
    }

    public void ClickRegionButton(int regionNum)
    {
        if (regionIsActive[regionNum])
        {
            GameObject.Find("Grid").GetComponent<CreateGrid>().UpdateRegionActive(regionNum);
        }

    }
    #endregion
}
