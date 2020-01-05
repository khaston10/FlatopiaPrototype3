using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGlossaryPlantEater : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Deals with user input.
    public void ClickBack()
    {
        SceneManager.LoadScene("StartScreen");
        Debug.Log("Switch Level");
    }


}
