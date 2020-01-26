using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    #region Variables
    public Transform gridPreFab;
    public Transform gridPreFab2;
    public Transform gridPreFab3;
    public Transform gridPreFab4;
    public Transform wall;
    public List<Vector3> positions = new List<Vector3>();
    public List<Vector3> region2Positions = new List<Vector3>();
    public List<Vector3> region3Positions = new List<Vector3>();
    public List<Vector3> region4Positions = new List<Vector3>();
    public float ySpeed = 20.0f;

    #region Wall Vraiables
    Transform wall1;
    Transform wall2;
    Transform wall3;
    Transform wall4;
    Transform wall5;
    Transform wall6;
    Transform wall7;
    Transform wall8;
    Transform wall9;
    Transform wall10;
    Transform wall11;
    Transform wall12;
    Transform wall13;
    Transform wall14;
    Transform wall15;
    Transform wall16;
    #endregion

    #region Camera Variables
    public Camera[] cameras;

    // This integer can have the values 0, 1, 2, 3.
    // For example, if it has the value 2, then Camera[2] is the active camera.
    public int activeCamera;

    public Vector3 camRotatePoint;
    public Vector3 camPos;

    public bool movingLeft = false;
    public bool movingRight = false;
    bool movingForward = false;
    bool movingBackward = false;
    #endregion

    #region Region Variables
    // This array has 4 elements, Only one should be true at a time. If regionActive[1] is true, than region 2 is active, and so on.
    public bool[] regionActive;
    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Set Region.
        regionActive[0] = true;

        // Set camera 0 to active.
        activeCamera = 0;

        // Disable Audio Listeners.
        cameras[1].GetComponent<AudioListener>().enabled = false;
        cameras[1].enabled = false;
        cameras[2].GetComponent<AudioListener>().enabled = false;
        cameras[2].enabled = false;
        cameras[3].GetComponent<AudioListener>().enabled = false;
        cameras[3].enabled = false;


        // Populate the positions list with all possible positions for grids
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().worldSize; i++)
        {
            for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().worldSize; j++)
            {
                Vector3 pos = new Vector3(i, 1.0f, j);
                positions.Add(pos);
            }
        }

        // Place grid prefabs at positions from grid.
        for (int i = 0; i < positions.Count; i++)
        {
            Transform t = Instantiate(gridPreFab);
            t.localPosition = positions[i];
        }

        // Move camera into correct location based on the world size.
        UpdateCameraPos(0);

        // Create retaining walls in world to keep plant eaters and meat eaters from getting stuck.
        wall1 = Instantiate(wall);
        wall2 = Instantiate(wall);
        wall3 = Instantiate(wall);
        wall4 = Instantiate(wall);

        wall1.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1, 2f, .3f);
        wall1.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f, 1f, -.5f);
        wall2.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1, 2f, .3f);
        wall2.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f, 1f,
            GameObject.Find("Game").GetComponent<GameMain>().worldSize - .5f);
        wall3.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1);
        wall3.localPosition = new Vector3(-.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f);
        wall4.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().worldSize + 1);
        wall4.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize - .5f, 1f, 
            (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f);

        // Set the rotation point for the camera to rotate around when the user operates camera.
        camRotatePoint = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2), 0f, (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            // Find the active camera.

            cameras[activeCamera].transform.RotateAround(camRotatePoint, Vector3.up, Input.GetAxis("Mouse X") * ySpeed);
        }

        // Get user input to control camera.
        if (Input.GetKeyDown(KeyCode.A) && movingLeft == false) movingLeft = true;

        if (Input.GetKeyUp(KeyCode.A) && movingLeft == true) movingLeft = false;

        if (Input.GetKeyDown(KeyCode.D) && movingRight == false) movingRight = true;

        if (Input.GetKeyUp(KeyCode.D) && movingRight == true) movingRight = false;

        if (Input.GetKeyDown(KeyCode.W) && movingForward == false) movingForward = true;

        if (Input.GetKeyUp(KeyCode.W) && movingForward == true) movingForward = false;

        if (Input.GetKeyDown(KeyCode.S) && movingBackward == false) movingBackward = true;

        if (Input.GetKeyUp(KeyCode.S) && movingBackward == true) movingBackward = false;


        // Move camera if user is controlling it.
        if (movingLeft) cameras[activeCamera].gameObject.transform.Translate(-Vector3.right * Time.deltaTime);
        
        if (movingRight) cameras[activeCamera].gameObject.transform.Translate(Vector3.right * Time.deltaTime);

        if (movingForward) cameras[activeCamera].gameObject.transform.Translate(Vector3.forward * Time.deltaTime);

        if (movingBackward) cameras[activeCamera].gameObject.transform.Translate(-Vector3.forward * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateCameraPos(activeCamera);
        }

    }

    // Update Grid when user presses the world size button.
    public void UpdateGrid()
    {
        // Populate the news positions when player pushed the world size button.
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().worldSize; i++)
        {
            for (int j = GameObject.Find("Game").GetComponent<GameMain>().worldSize - 1; j < GameObject.Find("Game").GetComponent<GameMain>().worldSize; j++)
            {
                Vector3 foodPos = new Vector3(i, 1.0f, j);
                Vector3 plantEaterPos = new Vector3(i, 1.1f, j);
                positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().foodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositions.Add(plantEaterPos);
            }
        }
        for (int i = GameObject.Find("Game").GetComponent<GameMain>().worldSize - 1; i < GameObject.Find("Game").GetComponent<GameMain>().worldSize; i++)
        {
            for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().worldSize; j++)
            {
                Vector3 foodPos = new Vector3(i, 1.0f, j);
                Vector3 plantEaterPos = new Vector3(i, 1.4f, j);
                positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().foodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositions.Add(plantEaterPos);
            }
        }

        // Place grid prefabs at positions from grid.
        for (int i = 2 * (GameObject.Find("Game").GetComponent<GameMain>().worldSize - 1); i < positions.Count; i++)
        {
            Transform t = Instantiate(gridPreFab);
            t.localPosition = positions[i];
        }

        // Place retaining walls in world to keep plant eaters and meat eaters from getting stuck.
        wall1.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize, 2f, .3f);
        wall1.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f, 1f, -.5f);
        wall2.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize, 2f, .3f);
        wall2.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f, 1f,
            GameObject.Find("Game").GetComponent<GameMain>().worldSize - .5f);
        wall3.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().worldSize);
        wall3.localPosition = new Vector3(-.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f);
        wall4.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().worldSize);
        wall4.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize - .5f, 1f,
            (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2) - .5f);


    }

    public void UpdateGridRegion2()
    {
        // Populate the news positions when player pushed the world size button.
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; i++)
        {
            for (int j = GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - 1; j < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i, 1.0f, j + 40);
                Vector3 plantEaterPos = new Vector3(i, 1.1f, j + 40);
                region2Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region2FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion2.Add(plantEaterPos);
            }
        }
        for (int i = GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - 1; i < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; i++)
        {
            for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i, 1.0f, j + 40);
                Vector3 plantEaterPos = new Vector3(i, 1.1f, j + 40);
                region2Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region2FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion2.Add(plantEaterPos);
            }
        }

        // Place grid prefabs at positions from grid.
        for (int i = 2 * (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - 1); i < region2Positions.Count; i++)
        {
            Transform t = Instantiate(gridPreFab2);
            t.localPosition = region2Positions[i];
        }

        // Place retaining walls in world to keep plant eaters and meat eaters from getting stuck.
        wall5.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1, 2f, .3f);
        wall5.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) - .5f, 1f, 39.5f);
        wall6.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1, 2f, .3f);
        wall6.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) - .5f, 1f,
            GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - .5f + 40f);
        wall7.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1);
        wall7.localPosition = new Vector3(-.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) + 39.5f);
        wall8.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1);
        wall8.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - .5f, 1f,
            (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) + 39.5f);

        

    }

    public void UpdateGridRegion3()
    {
        // Populate the news positions when player pushed the world size button.
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; i++)
        {
            for (int j = GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize - 1; j < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i + 40, 1.0f, j);
                Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j);
                region3Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region3FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion3.Add(plantEaterPos);
            }
        }
        for (int i = GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize - 1; i < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; i++)
        {
            for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i + 40, 1.0f, j);
                Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j);
                region3Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region3FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion3.Add(plantEaterPos);
            }
        }

        // Place grid prefabs at positions from grid.
        for (int i = 2 * (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize - 1); i < region3Positions.Count; i++)
        {
            Transform t = Instantiate(gridPreFab3);
            t.localPosition = region3Positions[i];
        }

        wall9.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1, 2f, .3f);
        wall9.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 39.5f, 1f, -.5f);
        wall10.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1, 2f, .3f);
        wall10.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 39.5f, 1f,
            GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize - .5f);
        wall11.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1);
        wall11.localPosition = new Vector3(39.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2));
        wall12.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1);
        wall12.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 39.5f, 1f,
            (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2));



    }

    public void UpdateGridRegion4()
    {
        // Populate the news positions when player pushed the world size button.
        for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; i++)
        {
            for (int j = GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize - 1; j < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i + 40, 1.0f, j + 40);
                Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j + 40);
                region4Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region4FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion4.Add(plantEaterPos);
            }
        }
        for (int i = GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize - 1; i < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; i++)
        {
            for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; j++)
            {
                Vector3 foodPos = new Vector3(i + 40, 1.0f, j + 40);
                Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j + 40);
                region4Positions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().region4FoodPositions.Add(foodPos);
                GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion4.Add(plantEaterPos);
            }
        }

        // Place grid prefabs at positions from grid.
        for (int i = 2 * (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize - 1); i < region4Positions.Count; i++)
        {
            Transform t = Instantiate(gridPreFab4);
            t.localPosition = region4Positions[i];
        }

        wall13.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1, 2f, .3f);
        wall13.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 39.5f, 1f, +39.5f);
        wall14.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1, 2f, .3f);
        wall14.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 39.5f, 1f,
            GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 39.5f);
        wall15.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1);
        wall15.localPosition = new Vector3(39.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40f);
        wall16.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1);
        wall16.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 39.5f, 1f,
            (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40f);



    }

    // Update camera position
    public void UpdateCameraPos(int cameraNum)
    {
        if (cameraNum == 0)
        {
            // Set the rotation point for the camera to rotate around when the user operates camera.
            camRotatePoint = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2), 0f, (GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2));

            Vector3 camPos = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2, GameObject.Find("Game").GetComponent<GameMain>().worldSize, -GameObject.Find("Game").GetComponent<GameMain>().worldSize / 2);
            cameras[0].transform.localPosition = camPos;
            cameras[0].transform.LookAt(camRotatePoint);
        }

        else if (cameraNum == 1)
        {
            // Set the rotation point for the camera to rotate around when the user operates camera.
            camRotatePoint = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2), 0f, (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) + 40);

            Vector3 camPos = new Vector3(-(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2), GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize, 40 + GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2);
            cameras[1].transform.localPosition = camPos;
            cameras[1].transform.LookAt(camRotatePoint);
        }

        else if (cameraNum == 2)
        {
            // Set the rotation point for the camera to rotate around when the user operates camera.
            camRotatePoint = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 40, 0f, (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2));

            Vector3 camPos = new Vector3(-(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 40, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2);
            cameras[2].transform.localPosition = camPos;
            cameras[2].transform.LookAt(camRotatePoint);
        }

        else if (cameraNum == 3)
        {
            // Set the rotation point for the camera to rotate around when the user operates camera.
            camRotatePoint = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40, 0f, (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40);

            Vector3 camPos = new Vector3(-(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40, GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize, 40 + (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2));
            cameras[3].transform.localPosition = camPos;
            cameras[3].transform.LookAt(camRotatePoint);
        }



    }

    public void UpdateRegionActive(int activeRegion)
    {
        // Set all regions to inactive, then set the correct one to active.
        for (int i = 0; i < regionActive.Length; i++) regionActive[i] = false;
        regionActive[activeRegion] = true;

        //Set the correct Camera active.
        cameras[activeRegion].enabled = true;

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
        cameras[activeRegion].enabled = true;

        activeCamera = activeRegion;

        UpdateCameraPos(activeRegion);
    }

    public void CreateNewRegion(int newRegion)
    {
        if (newRegion == 1)
        {
            // Populate the positions list with all possible positions for grids
            for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; i++)
            {
                for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize; j++)
                {
                    Vector3 pos = new Vector3(i, 1.0f, 40 + j);
                    Vector3 plantEaterPos = new Vector3(i, 1.1f, j + 40);
                    region2Positions.Add(pos);



                    // Add position to the regions food list.
                    GameObject.Find("Game").GetComponent<GameMain>().region2FoodPositions.Add(pos);
                    GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion2.Add(plantEaterPos);
                }
            }

            // Place grid prefabs at positions from grid.
            for (int i = 0; i < region2Positions.Count; i++)
            {
                Transform t = Instantiate(gridPreFab2);
                t.localPosition = region2Positions[i];
            }

            // Create retaining walls in world to keep plant eaters and meat eaters from getting stuck.
            wall5 = Instantiate(wall);
            wall6 = Instantiate(wall);
            wall7 = Instantiate(wall);
            wall8 = Instantiate(wall);

            wall5.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1, 2f, .3f);
            wall5.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) - .5f, 1f, 39.5f);
            wall6.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1, 2f, .3f);
            wall6.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) - .5f, 1f,
                GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - .5f + 40f);
            wall7.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1);
            wall7.localPosition = new Vector3(-.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) + 39.5f);
            wall8.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize + 1);
            wall8.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize - .5f, 1f,
                (GameObject.Find("Game").GetComponent<GameMain>().region2WorldSize / 2) + 39.5f);
        }

        else if (newRegion == 2)
        {
            // Populate the positions list with all possible positions for grids
            for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; i++)
            {
                for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize; j++)
                {
                    Vector3 pos = new Vector3(i + 40, 1.0f, j);
                    Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j);
                    region3Positions.Add(pos);

                    // Add position to the regions food list.
                    GameObject.Find("Game").GetComponent<GameMain>().region3FoodPositions.Add(pos);
                    GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion3.Add(plantEaterPos);
                }
            }

            // Place grid prefabs at positions from grid.
            for (int i = 0; i < region3Positions.Count; i++)
            {
                Transform t = Instantiate(gridPreFab3);
                t.localPosition = region3Positions[i];
            }

            // Create retaining walls in world to keep plant eaters and meat eaters from getting stuck.
            wall9 = Instantiate(wall);
            wall10 = Instantiate(wall);
            wall11 = Instantiate(wall);
            wall12 = Instantiate(wall);
            
            wall9.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1, 2f, .3f);
            wall9.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 39.5f, 1f,  - .5f);
            wall10.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1, 2f, .3f);
            wall10.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2) + 39.5f, 1f,
                GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize - .5f);
            wall11.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1);
            wall11.localPosition = new Vector3(39.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2));
            wall12.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 1);
            wall12.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize + 39.5f, 1f,
                (GameObject.Find("Game").GetComponent<GameMain>().region3WorldSize / 2));
        }

        else if (newRegion == 3)
        {
            // Populate the positions list with all possible positions for grids
            for (int i = 0; i < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; i++)
            {
                for (int j = 0; j < GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize; j++)
                {
                    Vector3 pos = new Vector3(i + 40, 1.0f, j + 40);
                    Vector3 plantEaterPos = new Vector3(i + 40, 1.1f, j + 40);
                    region4Positions.Add(pos);

                    // Add position to the regions food list.
                    GameObject.Find("Game").GetComponent<GameMain>().region4FoodPositions.Add(pos);
                    GameObject.Find("Game").GetComponent<GameMain>().plantEaterStartPositionsRegion4.Add(plantEaterPos);
                }
            }

            // Place grid prefabs at positions from grid.
            for (int i = 0; i < region4Positions.Count; i++)
            {
                Transform t = Instantiate(gridPreFab4);
                t.localPosition = region4Positions[i];
            }

            // Create retaining walls in world to keep plant eaters and meat eaters from getting stuck.
            wall13 = Instantiate(wall);
            wall14 = Instantiate(wall);
            wall15 = Instantiate(wall);
            wall16 = Instantiate(wall);

            wall13.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1, 2f, .3f);
            wall13.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 39.5f, 1f, + 39.5f);
            wall14.localScale = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1, 2f, .3f);
            wall14.localPosition = new Vector3((GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 39.5f, 1f,
                GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 39.5f);
            wall15.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1);
            wall15.localPosition = new Vector3(39.5f, 1f, (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40f);
            wall16.localScale = new Vector3(.3f, 2f, GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 1);
            wall16.localPosition = new Vector3(GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize + 39.5f, 1f,
                (GameObject.Find("Game").GetComponent<GameMain>().region4WorldSize / 2) + 40f);
        }

    }
}
