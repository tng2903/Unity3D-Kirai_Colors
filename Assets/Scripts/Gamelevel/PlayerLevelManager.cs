﻿using UnityEngine;
using System.Collections;

public class PlayerLevelManager : MonoBehaviour {

    static int CurrentLevel;
    public static int currentLevel
    {
        get { return CurrentLevel; }
    }

    static int CurrentExp;
    public static int currentExp
    {
        get { return CurrentExp; }
    }

    static int ExpToNextLevel;
    public static int expToNextLevel
    {
        get { return ExpToNextLevel; }
    }
    //exp before add operation
    static int OldExp;
    public static int oldExp
    {
        get { return OldExp; }
    }

    public static bool isRankUp = false;


    static string Save_currentlevel = "CurrentLevel";
    static string Save_currentxp = "CurrentExp";

    static int maxLevel = 100;


  

    static void GetSaveData()
    {
        if (PlayerPrefs.HasKey(Save_currentlevel))
        {
            CurrentLevel = PlayerPrefs.GetInt(Save_currentlevel);
        }
        else
        {
            CurrentLevel = 0;
            PlayerPrefs.SetInt(Save_currentlevel, CurrentLevel);
        }

        if (PlayerPrefs.HasKey("CurrentExp"))
        {
            CurrentExp = PlayerPrefs.GetInt(Save_currentxp);
        }
        else
        {
            CurrentExp = 0;
            PlayerPrefs.SetInt(Save_currentxp, CurrentExp);
        }
    }


    static void CalculateExpForNextLevel()
    {
        ExpToNextLevel = 100 * CurrentLevel + 100;
        //Debug.Log(expToNextLevel);
    }

	// Use this for initialization
	void Start () {
        PlayerLevelManager.GetSaveData();
        PlayerLevelManager.CalculateExpForNextLevel();
	}

    //add exp to user and caculate eventual rankup
    public static void AddExp(int expToAdd)
    {
        OldExp = CurrentExp;

        if (CurrentLevel + 1 <= maxLevel)
        {
            int temp = CurrentExp + expToAdd;
            Debug.Log(temp + " of " + ExpToNextLevel);
            if (temp >= ExpToNextLevel)
            {
                while (true)
                {
                    CurrentLevel++;
                    CurrentExp = temp - ExpToNextLevel;
                    isRankUp = true;
                    Debug.Log("Rank Up! Current exp:" + currentExp);
                    CalculateExpForNextLevel();
                    if (expToAdd < expToNextLevel)
                        break;
                }
            }
            else
            {
                CurrentExp = temp;
                isRankUp = false;
                Debug.Log("Current exp:" + currentExp);
            }
            SaveData();
        }
    }

    //save all data to disk
    static void SaveData()
    {
        PlayerPrefs.SetInt(Save_currentlevel, CurrentLevel);
        PlayerPrefs.SetInt(Save_currentxp, CurrentExp);
    }

    public static int CalculateExpForLevel(int level)
    {
        return 100 * level + 100;
    }
}
