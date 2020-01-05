using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantEaterAnimationControllerGloss : MonoBehaviour
{
    public Transform plantPrefab;
    public Transform plantEaterPrefab;
    public Transform plantEaterBabyPrefab;
    public Text advancedInformationText;
    public Animator animBaby;
    Animator animPlantEater;
    
    

    // Start is called before the first frame update
    void Start()
    {
        animPlantEater = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ClickSurvive()
    {
        
        animPlantEater.Play("PlantEaterGlossAnim");
        Transform t = Instantiate(plantPrefab);
        t.localPosition = new Vector3(-1f, -.2f, -7f);
        advancedInformationText.text = "At least one plant must be eaten for a plant eater to survive the day.";

        // If baby has been put front and center on screen because the user clicked the Reproduce button we will move it back out of view. 
        animBaby.Play("BabyPlantEaterOffScreenAnim");
    }

    public void ClickReproduce()
    {
        // If baby has been put front and center on screen because the user clicked the Reproduce button we will move it back out of view. 
        animBaby.Play("BabyPlantEaterOffScreenAnim");

        animPlantEater.Play("PlantEaterGlossReproduceAnim");
        Transform t = Instantiate(plantPrefab);
        t.localPosition = new Vector3(-.5f, -.2f, -7f);
        Transform j = Instantiate(plantPrefab);
        j.localPosition = new Vector3(-1.5f, -.2f, -7f);
        advancedInformationText.text = "A plant eater will reproduce if two plants are eaten.";

    }

    public void MakeBaby()
    {
        // Play the animation to bring plantEaterBaby into scene.
        animBaby.Play("BabyPlantEaterIdleAnim");

    }
}
