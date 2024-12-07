using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime = 0;
    bool timerOn = false;

    public void OnTimerPlay()
    {
        timerOn = true;
    }

    public void OnTimerPause()
    {
        timerOn = false;
    }

    public void OnTimerReset()
    {
        currentTime = 0;
        timerOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            currentTime += Time.deltaTime;
        }
    }

    public string GetTime()
    {
        //Formats current time into the form 0:00, the D2 specifies fill with zeroes, could add to first term.
        return string.Format("{0}:{1:D2}", (int)currentTime/60, (int)currentTime%60);
    }

    public void NextLevel() //Adds the level's time to the total (if we have the ability to go back to old levels, I'll rework this to take best time of each level)
    {
        PlayerPrefs.SetFloat("TotalTime", PlayerPrefs.GetFloat("TotalTime") + currentTime);
    }
}
