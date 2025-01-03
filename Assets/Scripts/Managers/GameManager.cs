using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton -------------------
    /// </summary>
    private static GameManager instance;

    public static GameManager GetInstance()
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

    private void Awake()
    {
        SetSingleton();
    }

    /// <summary>
    /// End of the Singleton -------------------
    /// </summary>

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void helloWorld() {
        Debug.Log("Hello world.");
    }
}
