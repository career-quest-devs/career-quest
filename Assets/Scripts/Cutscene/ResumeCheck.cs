using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResumeCheck : MonoBehaviour
{
    [SerializeField] private GameObject _resume;
    [SerializeField] private GameObject _fly;
    [SerializeField] private GameObject _gameOptions;
 
    [SerializeField] private GameObject _normal;
    [SerializeField] private GameObject _tie;
    [SerializeField] private GameObject _socks;
    [SerializeField] private GameObject _tieAndSocks;

    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _sneezeText;
    [SerializeField] private TMP_Text _helloText;
    [SerializeField] private TMP_Text _dashText;

    // Start is called before the first frame update
    void Start()
    {
        _gameOptions.SetActive(false);

        DataTracker data = DataTracker.GetInstance();

        //data.SetLevelTime(1, 100);
        //data.HasResume = true;
        //data.IncrementAbility("Sneeze");
        //data.IncrementAbility("Sneeze");
        //data.IncrementAbility("Sneeze");
        //data.IncrementAbility("Wave");
        //data.IncrementAbility("Dash");

        if (data.HasResume)
        {
            _resume.SetActive(true);
            _fly.SetActive(false);

            int minutes = Mathf.FloorToInt(data.GetTotalTime() / 60);
            int seconds = Mathf.FloorToInt(data.GetTotalTime() % 60);
            _timeText.text = "Time taken: " + $"{minutes:00}:{seconds:00}";

            _sneezeText.text = "Sneezes Made: " + data.GetAbilityTotal("Sneeze");
            _helloText.text = "Said Hello: " + data.GetAbilityTotal("Wave");
            _dashText.text = "Times Dashed: " + data.GetAbilityTotal("Dash");
        }
        else
        {
            _resume.SetActive(false);
            _fly.SetActive(true);
        }

        StartCoroutine(DisplayGameOptions());
    }

    private IEnumerator DisplayGameOptions()
    {
        yield return new WaitForSeconds(8.0f);
        _gameOptions.SetActive(true);
    }
}
