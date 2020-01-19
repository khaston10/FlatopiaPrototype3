using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatEaterContoller : MonoBehaviour
{
    public int timeBetweenDirectionChange = 1;
    public int daysSinceLastEaten = 0;
    public Vector3 relativePos;
    public Quaternion rotation;
    public bool isAwake;
    

    public int direction = 0; // 0: Move y+, 1: Move y-, 2: Move x+, 3: Move x-
    private float timer = 0.0f;
    private Color FedMeatEater = new Color(.55f, .33f, .9f, 1f);
    public AudioClip EatSound;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables.
        relativePos = new Vector3(0f, 0f, 0f);
        rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject.Find("Game").GetComponent<GameMain>().visionDistance = 5;
        isAwake = true;
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
            MeatEaterChangeDirection();
            timer = 0;
        }

        // Move plant eater if creatures are awake.
        if (isAwake && GameObject.Find("Game").GetComponent<GameMain>().gamePaused != true 
            && GameObject.Find("Game").GetComponent<GameMain>().meatEatersFrozen == false)
        {
            MeatEaterMove();
        }

    }
    public void MeatEaterChangeDirection()
    {
        // Check to see if food is in eye sight.
        if(GameObject.Find("Game").GetComponent<GameMain>().plantEaterList.Count > 0)
        {
            for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().plantEaterList.Count; i++)
            {
                if (Vector3.Distance(transform.position, GameObject.Find("Game").GetComponent<GameMain>().plantEaterList[i].position) <= GameObject.Find("Game").GetComponent<GameMain>().visionDistance)
                {
                    // Save the plant eater position.
                    relativePos = GameObject.Find("Game").GetComponent<GameMain>().plantEaterList[i].position - transform.position;
                    relativePos.y = 0f;
                    direction = 4;
                }
            }
        }
        

        // Pick random direction if it has not already been picked.
        if (direction != 4)
        {
            relativePos = transform.position;
            relativePos.y = 0f;

            direction = Random.Range(0, 4);

            if (direction == 0) relativePos.x += 50;
            else if (direction == 1) relativePos.x -= 50;
            else if (direction == 2) relativePos.z += 50;
            else if (direction == 3) relativePos.z -= 50;
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

    public void MeatEaterMove()
    {
        transform.Translate(Vector3.forward * GameObject.Find("Game").GetComponent<GameMain>().meatEaterSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "meatEater")
        {
            Physics.IgnoreCollision(col.collider, GetComponent<Collider>());
        }

        else if (col.gameObject.tag == "plantEater" && GameObject.Find("Game").GetComponent<GameMain>().meatEatersFrozen == false)
        {
            AudioSource.PlayClipAtPoint(EatSound, transform.position);
            GameObject.Find("Game").GetComponent<GameMain>().plantEaters -= 1;
            GameObject.Find("Game").GetComponent<GameMain>().plantEaterList.Remove(col.transform);
            Destroy(col.transform.gameObject);
            daysSinceLastEaten = 0;
            transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = FedMeatEater;

            //Reset the stat used for the ninja achievement.
            GameObject.Find("Game").GetComponent<GameMain>().noPlantEatersKilledForXDays = 0;
        }
    }
}


