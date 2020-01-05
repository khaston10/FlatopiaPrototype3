using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedUpgrades : MonoBehaviour
{
    // Buttons from panel.
    public Button S1;
    public Button S2;
    public Button S3;
    public Button S4;
    public Button S5;
    public Button M1;
    public Button M2;
    public Button M3;
    public Button M4;
    public Button M5;

    // Text from panel.
    public Text advacedUpgradeText;

    // Panel
    public GameObject advancedUpgradePanel;

    // Prefabs
    public Transform militaryOutpost;
    public Transform militaryOutpost2;
    public Transform scienceOutpost;
    public Transform scienceOutpost2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && advancedUpgradePanel.activeInHierarchy == true)
        {
            advancedUpgradePanel.SetActive(false);
        }
    }

    public void ExitPanel()
    {
        advancedUpgradePanel.SetActive(false);
    }

    public void HoverS1()
    {
        advacedUpgradeText.text = "Cost: 20 - Get Research Facility\nRequires World Size 10 or larger";
    }

    public void HoverS2()
    {
        advacedUpgradeText.text = "Cost: 20 - Get New Research Facility\nRequires Science 1 & Research: 50";
    }

    public void HoverS3()
    {
        advacedUpgradeText.text = "This feature is not ready.";
    }

    public void HoverM1()
    {
        advacedUpgradeText.text = "Cost: 10 - Get Military Outpost\nResearchPoints: 10";
    }

    public void HoverM2()
    {
        advacedUpgradeText.text = "Cost: 20 - Get New Military Facility\nRequires Military 1 & Research: 100";
    }

    public void HoverM3()
    {
        advacedUpgradeText.text = "This feature is not ready.";
    }

    public void ClearText()
    {
        advacedUpgradeText.text = "";
    }

    public void ClickS1()
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().gamePoints >= 20 && GameObject.Find("Game").GetComponent<GameMain>().s1Unlocked == false)
        {
            if (GameObject.Find("Game").GetComponent<GameMain>().worldSize >= 10)
            {
                S1.GetComponent<Image>().color = Color.green;
                GameObject.Find("Game").GetComponent<GameMain>().gamePoints -= 20;
                GameObject.Find("Game").GetComponent<GameMain>().s1Unlocked = true;

                // Create the Science Outpost.
                Transform s = Instantiate(scienceOutpost);
                Vector3 outpostPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1, 1.8f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
                s.localPosition = outpostPosition;
            }

            else
            {
                advacedUpgradeText.text = "World Size not large enough. Need a minimum of 10.";
            }

        }
    }

    public void ClickS2()
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().gamePoints >= 20 && GameObject.Find("Game").GetComponent<GameMain>().s2Unlocked == false && 
            GameObject.Find("Game").GetComponent<GameMain>().researchPoints >= 50 && GameObject.Find("Game").GetComponent<GameMain>().s1Unlocked == true)
        {
            if (GameObject.Find("Game").GetComponent<GameMain>().worldSize >= 10)
            {
                S2.GetComponent<Image>().color = Color.green;
                GameObject.Find("Game").GetComponent<GameMain>().gamePoints -= 20;
                GameObject.Find("Game").GetComponent<GameMain>().s2Unlocked = true;
                GameObject.Find("Game").GetComponent<GameMain>().researchPoints -= 50;

                //Update the research points panel.
                GameObject.Find("Game").GetComponent<GameMain>().UpdateResearchPoints();

                //Delete old outpost.
                Destroy(GameObject.Find("ScienceOutpost(Clone)"));
                Destroy(GameObject.Find("Plus1(Clone)"));
                

                // Create the Science Outpost 2.
                Transform s = Instantiate(scienceOutpost2);
                Vector3 outpostPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1, 1.8f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
                s.localPosition = outpostPosition;
            }

            else
            {
                advacedUpgradeText.text = "World Size not large enough. Need a minimum of 10.";
            }

        }

    }

    public void ClickM1()
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().gamePoints > 10 && GameObject.Find("Game").GetComponent<GameMain>().m1Unlocked == false 
            && GameObject.Find("Game").GetComponent<GameMain>().researchPoints >= 10)
        {
            if (GameObject.Find("Game").GetComponent<GameMain>().worldSize >= 10)
            {
                M1.GetComponent<Image>().color = Color.green;
                GameObject.Find("Game").GetComponent<GameMain>().gamePoints -= 10;
                GameObject.Find("Game").GetComponent<GameMain>().m1Unlocked = true;
                GameObject.Find("Game").GetComponent<GameMain>().researchPoints -= 10;

                //Update the research points panel.
                GameObject.Find("Game").GetComponent<GameMain>().UpdateResearchPoints();

                // Create the Military Outpost.
                Transform m = Instantiate(militaryOutpost);
                Vector3 outpostPosition = new Vector3(-2f, 1.8f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
                m.localPosition = outpostPosition;
            }

            else
            {
                advacedUpgradeText.text = "World Size not large enough. Need a minimum of 10.";
            }
            

        }
    }

    public void ClickM2()
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().gamePoints > 20 && GameObject.Find("Game").GetComponent<GameMain>().m2Unlocked == false
            && GameObject.Find("Game").GetComponent<GameMain>().researchPoints >= 100 && GameObject.Find("Game").GetComponent<GameMain>().m1Unlocked == true)
        {

            M2.GetComponent<Image>().color = Color.green;
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints -= 20;
            GameObject.Find("Game").GetComponent<GameMain>().m2Unlocked = true;
            GameObject.Find("Game").GetComponent<GameMain>().researchPoints -= 100;

            //Update the research points panel.
            GameObject.Find("Game").GetComponent<GameMain>().UpdateResearchPoints();

            // Destory old Military outpost.
            //Delete old outpost.
            Destroy(GameObject.Find("MilitaryOutpost(Clone)"));
            Destroy(GameObject.Find("B(Clone)"));

            // Create the Military Outpost.
            Transform m = Instantiate(militaryOutpost2);
            Vector3 outpostPosition = new Vector3(-2f, 1.8f, GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
            m.localPosition = outpostPosition;

        }
    }


}
