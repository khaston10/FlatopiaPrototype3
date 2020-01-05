using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeatEaterAnimationControllerGloss : MonoBehaviour
{
    public Text advancedInformationText;
    public GameObject trigger;
    public Transform plantEaterPrefab;
    
    Animator animMeatEater;



    // Start is called before the first frame update
    void Start()
    {
        animMeatEater = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickSurvive()
    {

        animMeatEater.Play("MeatEaterEatAnim");
        Transform t = Instantiate(plantEaterPrefab);
        t.localPosition = new Vector3(-1f, .3f, -7f);
        t.localRotation = Quaternion.Euler(0, -100, 0);
        advancedInformationText.text = "At least one plant eater must be eaten for a meat eater to survive the day.";

        // Create trigger to destroy plant eater.


        // If baby has been put front and center on screen because the user clicked the Reproduce button we will move it back out of view. 
        //animBaby.Play("BabyPlantEaterOffScreenAnim");
    }

}
