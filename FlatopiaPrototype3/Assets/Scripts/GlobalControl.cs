using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int worldSize;
    public int foodSpawned;
    public int plantEaters;
    public int gamePoints;
    public bool landOwnerAchievement;
    public bool genocideAchievement;
    public bool ninjaAchievement;
    public bool survivalistAchievement;
    public bool glutonAchievement;
    public bool unlockedAchievement;
    public bool gameWon;
    public int day;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
