using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTracker : MonoBehaviour
{
    /// <summary>
    /// Singleton -------------------
    /// </summary>
    private static DataTracker instance;

    public static DataTracker GetInstance()
    {
        return instance;
    }

    void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    /// <summary>
    /// End of the Singleton -------------------
    /// </summary>

    void Awake()
    {
        SetSingleton();
        DontDestroyOnLoad(this);
    }

    Dictionary<int, float> totalTime = new Dictionary<int, float>(); // Intialized by each level.

    Dictionary<string, int> data = new Dictionary<string, int> // I've intialized them here, as there should only be a set amount
    {
        {"sneeze", 0},
        {"hello", 0}
    };

    public void SetLevelTime(int levelIndex, float time)
    {
        if (totalTime.ContainsKey(levelIndex))
        {
            if (totalTime[levelIndex] > time)
            {
                totalTime[levelIndex] = time;
            }
        }
        else
        {
            totalTime[levelIndex] = time;
        }
    }

    public float GetTotalTime()
    {
        float total = 0;
        foreach (float value in totalTime.Values)
        {
            total += value;
        }
        return total;
    }

    public void IncrementAbility(string abilityName)
    {
        data[abilityName] += 1;
    }

    public int GetAbilityTotal(string abilityName)
    {
        return data[abilityName];
    }
}
