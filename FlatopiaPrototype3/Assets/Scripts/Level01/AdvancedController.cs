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

}
