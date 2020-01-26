using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private Color FedPlantEater = new Color(.1f, 1f, .1f, 1f);
    private Color starvingPlantEater = new Color(.8f, .7f, .3f, .1f);
    public AudioClip EatSound;
    public bool colliderActive = false;
    public Animator Anim;

    // There are 4 different regions in the game. 0: Assigned to region1, 1: assigned to region 2, ...
    public int regionAssigned = 0;


    // Start is called before the first frame update
    void Start()
    {
        // add isTrigger
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the PlantGrow animation is playing. If it is not, then the tigger should be active.
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("PlantIdle1") || Anim.GetCurrentAnimatorStateInfo(0).IsName("PlantIdle2") 
            || Anim.GetCurrentAnimatorStateInfo(0).IsName("PlantIdle3") || Anim.GetCurrentAnimatorStateInfo(0).IsName("PlantIdle4") && colliderActive == false)
        {
            colliderActive = true;

        }
            
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plantEater") && colliderActive  && regionAssigned == 0 &&(other.GetComponent<PlantEaterContoller>().foodEaten <= 2 || 
            GameObject.Find("Game").GetComponent<GameMain>().caffeineSpeedOn))
        {

            colliderActive = false;
            AudioSource.PlayClipAtPoint(EatSound, transform.position);

            // Add plant's position back to food position list. 
            GameObject.Find("Game").GetComponent<GameMain>().foodPositions.Add(transform.localPosition);
            GameObject.Find("Game").GetComponent<GameMain>().foodList.Remove(transform);
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints += 1;
            GameObject.Find("Game").GetComponent<GameMain>().gamePointsText.text = GameObject.Find("Game").GetComponent<GameMain>().gamePoints.ToString();
            Destroy(transform.gameObject);
            other.GetComponent<PlantEaterContoller>().foodEaten += 1;

            if (other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color == starvingPlantEater)
            {
                other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedPlantEater;
            } 
        }

        else if (other.CompareTag("plantEater") && colliderActive && regionAssigned == 1 && (other.GetComponent<PlantEaterContoller>().foodEaten <= 2 ||
            GameObject.Find("Game").GetComponent<GameMain>().caffeineSpeedOn ))
        {

            colliderActive = false;
            AudioSource.PlayClipAtPoint(EatSound, transform.position);

            // Add plant's position back to food position list. 
            GameObject.Find("Game").GetComponent<GameMain>().region2FoodPositions.Add(transform.localPosition);
            GameObject.Find("Game").GetComponent<GameMain>().region2FoodList.Remove(transform);
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints += 1;
            GameObject.Find("Game").GetComponent<GameMain>().gamePointsText.text = GameObject.Find("Game").GetComponent<GameMain>().gamePoints.ToString();
            Destroy(transform.gameObject);
            other.GetComponent<PlantEaterContoller>().foodEaten += 1;

            if (other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color == starvingPlantEater)
            {
                other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedPlantEater;
            }
        }

        else if (other.CompareTag("plantEater") && colliderActive && regionAssigned == 2 && (other.GetComponent<PlantEaterContoller>().foodEaten <= 2 ||
            GameObject.Find("Game").GetComponent<GameMain>().caffeineSpeedOn))
        {

            colliderActive = false;
            AudioSource.PlayClipAtPoint(EatSound, transform.position);

            // Add plant's position back to food position list. 
            GameObject.Find("Game").GetComponent<GameMain>().region3FoodPositions.Add(transform.localPosition);
            GameObject.Find("Game").GetComponent<GameMain>().region3FoodList.Remove(transform);
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints += 1;
            GameObject.Find("Game").GetComponent<GameMain>().gamePointsText.text = GameObject.Find("Game").GetComponent<GameMain>().gamePoints.ToString();
            Destroy(transform.gameObject);
            other.GetComponent<PlantEaterContoller>().foodEaten += 1;

            if (other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color == starvingPlantEater)
            {
                other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedPlantEater;
            }
        }

        else if (other.CompareTag("plantEater") && colliderActive && regionAssigned == 3 && (other.GetComponent<PlantEaterContoller>().foodEaten <= 2 ||
            GameObject.Find("Game").GetComponent<GameMain>().caffeineSpeedOn))
        {

            colliderActive = false;
            AudioSource.PlayClipAtPoint(EatSound, transform.position);

            // Add plant's position back to food position list. 
            GameObject.Find("Game").GetComponent<GameMain>().region4FoodPositions.Add(transform.localPosition);
            GameObject.Find("Game").GetComponent<GameMain>().region4FoodList.Remove(transform);
            GameObject.Find("Game").GetComponent<GameMain>().gamePoints += 1;
            GameObject.Find("Game").GetComponent<GameMain>().gamePointsText.text = GameObject.Find("Game").GetComponent<GameMain>().gamePoints.ToString();
            Destroy(transform.gameObject);
            other.GetComponent<PlantEaterContoller>().foodEaten += 1;

            if (other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color == starvingPlantEater)
            {
                other.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedPlantEater;
            }
        }

        else
        {
            Debug.Log("No Worries");
        }
        
     
       
    }
}
