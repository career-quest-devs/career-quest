using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniLevelScroll : MonoBehaviour
{
    [SerializeField] private float translateSpeed;
    
    public List<Transform> miniLevels = new List<Transform>();

    Vector3 lastLevel;
    Vector3 nextLevel;

    Dictionary<int, int> levelToTransform = new Dictionary<int, int> // Change indexes
    {
        { 2, 1 }, { 3, 2 }, { 4, 3 }
    };

    bool notArrived = true;

    void Start()
    {
        int level = DataTracker.GetInstance().lastSceneIndex;
        Debug.Log(levelToTransform[level]);
        lastLevel = miniLevels[levelToTransform[level]-1].position;
        nextLevel = miniLevels[levelToTransform[level]].position;
        gameObject.transform.position = lastLevel;
    }

    void Update()
    {
        if (notArrived)
        {
            gameObject.transform.Translate(Time.deltaTime * translateSpeed * (nextLevel - lastLevel).normalized);
            if (Vector3.Distance(nextLevel, transform.position) <= 0.2)
            {
                notArrived = false;
                StartCoroutine(StartNextLevel());
            }
        }
    }

    IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(3);

        int nextSceneIndex = DataTracker.GetInstance().lastSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
