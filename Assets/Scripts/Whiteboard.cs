using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Sprite[] cleanStages;

    private SpriteRenderer _sr;
    private int _currentStage = 0;

    public void CleanWhiteboard()
    {
        if (_currentStage < 3)
        {
            _currentStage++;
            _sr.sprite = cleanStages[_currentStage];
        }
    }

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }
}