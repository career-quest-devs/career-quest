using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneChange.GetInstance().NextScene();
    }
}
