using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DictionaryDialogs : MonoBehaviour
{
    /// <summary>
    /// This function returns the string written in the file DictionaryDialog in the position [Scene # (Inputted by file)][Line # of text in scene]
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetText(int index)
    {
        Scene scene = SceneManager.GetActiveScene();
        return dictionary[scene.buildIndex][index];
    }

    private Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>> 
    {
        [0] = new List<string> // Scene 0's dialog
        {
            "First Scene Dialog",
            "First Scene Second Dialog"
        }
        ,
        [1] = new List<string> // Scene 1's dialog
        {
            "Second Scene Dialog"
        }
        ,
        [2] = new List<string> // Scene 2's dialog
        {
            "Third Scene Dialog"
        }
    };


    // Down here is testing purposes and will be removed when we hook up build indexes to the proper scenes
    private List<string> testDialog = new List<string>
    {
        "First Dialog!",
        "1\n2\n3",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna."
    };

    public string GetTestText(int index)
    {
        return testDialog[index];
    }
}
