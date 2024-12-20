using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    /// <summary>
    /// Singleton -------------------
    /// </summary>
    private static SceneChange instance;

    public static SceneChange GetInstance()
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

    [SerializeField] private int miniLevelIndex;

    public int lastScene;

    DataTracker data;
    List<int> loadNext = new List<int> { 0,1 };

    private void Start()
    {
        data = DataTracker.GetInstance();
    }

    public void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int current = currentScene.buildIndex;
        
        lastScene = current;
        if (loadNext.Contains(current))
        {
            // Fade to black
            SceneManager.LoadScene(current + 1);
        }
        else
        {
            // Mini Level
            SceneManager.LoadScene(miniLevelIndex);
        }
    }

    public void MiniNextScene()
    {
        SceneManager.LoadScene(lastScene + 1);
    }
}
