using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Whiteboard : MonoBehaviour
{
    public UnityEvent OnWhiteboardCleaned;

    public Sprite[] cleanStages;

    private SpriteRenderer _sr;
    private int _currentStage = 0;

    public void CleanWhiteboard()
    {
        if (_currentStage < 3)
        {
            _currentStage++;
            _sr.sprite = cleanStages[_currentStage];

            if (_currentStage == 3)
            {
                // Whiteboard is clean
                OnWhiteboardCleaned?.Invoke();
            }
        }
    }

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
}
