using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEaterContoller : MonoBehaviour
{
    public int timeBetweenDirectionChange = 1;
    public int foodEaten = 1;
    public bool isStarving = false;
    public Vector3 relativePos;
    public Quaternion rotation;
    public int visionDistance;
    public bool isAwake;

    public int direction = 0; // 0: Move y+, 1: Move y-, 2: Move x+, 3: Move x-
    private float timer = 0.0f;
    private Animation anim;
    



    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables.
        relativePos = new Vector3(0f, 0f, 0f);
        rotation = new Quaternion(0f, 0f, 0f, 0f);
        visionDistance = 5;
        isAwake = true;

        // Set Walk Animation to correct speed.
        anim = transform.GetChild(0).GetComponent<Animation>();
        anim["walk"].speed = 2;
   
    }

    // Update is called once per frame
    void Update()
    {

        // Start timer for the plant eater's movement.
        if (GameObject.Find("Game").GetComponent<GameMain>().gamePaused != true)
        {
            timer += Time.deltaTime;
        }

        // Check to see if it is time to update plant eater's direction.
        if (timeBetweenDirectionChange < timer && isAwake)
        {
            PlantEaterChangeDirection();
            timer = 0;
        }

        // Move plant eater if creatures are awake.
        if (isAwake && GameObject.Find("Game").GetComponent<GameMain>().gamePaused != true)
        {
            PlantEaterMove();
        }      
    }

    public void PlantEaterChangeDirection()
    {
        // Check to see if food is in eye sight.
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().foodList.Count; i++)
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Game").GetComponent<GameMain>().foodList[i].position) <= visionDistance)
            {
                // Save the food's position.
                relativePos = GameObject.Find("Game").GetComponent<GameMain>().foodList[i].position - transform.position;
                relativePos.y = 0f;
                direction = 4;
            }

        }

        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region2FoodList.Count; i++)
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Game").GetComponent<GameMain>().region2FoodList[i].position) <= visionDistance)
            {
                // Save the food's position.
                relativePos = GameObject.Find("Game").GetComponent<GameMain>().region2FoodList[i].position - transform.position;
                relativePos.y = 0f;
                direction = 4;
            }

        }

        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region3FoodList.Count; i++)
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Game").GetComponent<GameMain>().region3FoodList[i].position) <= visionDistance)
            {
                // Save the food's position.
                relativePos = GameObject.Find("Game").GetComponent<GameMain>().region3FoodList[i].position - transform.position;
                relativePos.y = 0f;
                direction = 4;
            }

        }

        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region4FoodList.Count; i++)
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Game").GetComponent<GameMain>().region4FoodList[i].position) <= visionDistance)
            {
                // Save the food's position.
                relativePos = GameObject.Find("Game").GetComponent<GameMain>().region4FoodList[i].position - transform.position;
                relativePos.y = 0f;
                direction = 4;
            }

        }

        // Pick random direction if it has not already been picked.
        if (direction != 4)
        {
            relativePos = transform.position;
            relativePos.y = 0f;

            direction = Random.Range(0, 4);

            if (direction == 0) relativePos.x += 100;
            else if (direction == 1) relativePos.x -= 100;
            else if (direction == 2) relativePos.z += 100;
            else if (direction == 3) relativePos.z -= 100;
        }
        
        // Rotate the plant eater.
        // the second argument, upwards, defaults to Vector3.up
        rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        // Reset the direction variable. This will help in the case that the plant eater eats the last plant.
        if (direction == 4)
        {
            direction = Random.Range(0, 4);
        }



    } 

    public void PlantEaterMove()
    {
        if (GameObject.Find("Game").GetComponent<GameMain>().caffeineSpeedOn)
        {
            transform.Translate(Vector3.forward * GameObject.Find("Game").GetComponent<GameMain>().caffeinePlantEaterSpeed * Time.deltaTime);
        }
        else transform.Translate(Vector3.forward * GameObject.Find("Game").GetComponent<GameMain>().plantEaterSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision col)
    {
       
        if (col.gameObject.tag == "plantEater")
        {
            //Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }


    }
}
