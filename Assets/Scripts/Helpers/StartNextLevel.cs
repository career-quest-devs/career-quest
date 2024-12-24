using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Save game data to DataTracker
        DataTracker.GetInstance().lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Go to next scene
        SceneManager.LoadScene("MiniLevels");
    }
}
