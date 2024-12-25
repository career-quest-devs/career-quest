using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int startTime = 1800; // Set countdown start time (in seconds)
    private int initialTime;
    public float timeRemaining;

    private bool _timerOn = false;

    public void OnTimerPlay()
    {
        _timerOn = true;
    }

    public void OnTimerPause()
    {
        _timerOn = false;
    }

    public void OnTimerReset()
    {
        initialTime = startTime;
        timeRemaining = startTime;
        _timerOn = false;
    }

    private void Start()
    {
        initialTime = startTime;
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerOn)
        {
            // Decrease time remaining
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0.0f)
            {
                timeRemaining = 0.0f;
                _timerOn = false;
            }
        }
    }

    public string GetTime()
    {
        // Convert time remaining to minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        //Formats current time into the form 0:00, the D2 specifies fill with zeroes, could add to first term.
        //return string.Format("{0}:{1:D2}", (int)_currentTime/60, (int)_currentTime%60);
        return $"{minutes:00}:{seconds:00}";
    }

    public int GetTimeTaken()
    {
        return initialTime - Mathf.FloorToInt(timeRemaining);
    }
}
