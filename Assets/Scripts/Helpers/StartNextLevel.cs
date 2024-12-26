using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataTracker dataTracker = DataTracker.GetInstance();

        // Save game data to DataTracker
        dataTracker.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        dataTracker.SetLevelTime(3, 22.0f);

        // Go to next scene
        SceneManager.LoadScene("MiniLevels");
    }
}
