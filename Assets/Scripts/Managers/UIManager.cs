using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TMP_Text _dialogText;

    [Header("Timer UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer _timer;

    [Header("Mobile Controls")]
    [SerializeField] private GameObject _mobileControls;
    [SerializeField] private GameObject _sneezeButton;
    [SerializeField] private GameObject _waveButton;
    [SerializeField] private GameObject _dashButton;
    [SerializeField] private GameObject _pickUpButton;
    [SerializeField] private GameObject _openButton;
    [SerializeField] private MobileDetector _mobileDetector;

    [Header("Player")]
    [SerializeField] private PlayerInteract _playerInteract;

    private Queue<string> _dialogQueue;

    public bool IsRunningOnMobile()
    {
        return _mobileDetector.IsRunningOnMobile();
    }

    public void StartDialog(string[] dialogLines)
    {
        _dialogQueue.Clear();
        foreach (string line in dialogLines)
        {
            _dialogQueue.Enqueue(line);
        }

        _dialogBox.SetActive(true);
        DisplayNextLine();
    }

    public bool DisplayNextLine()
    {
        if (_dialogQueue.Count == 0)
        {
            EndDialog();
            return false;
        }

        string nextLine = _dialogQueue.Dequeue();
        _dialogText.text = nextLine;
        return true;
    }

    public void EndDialog()
    {
        _dialogBox.SetActive(false);
    }

    public void SetSneezeButtonVisibility(bool isVisible)
    {
        _sneezeButton.SetActive(isVisible);
    }

    public void SetWaveButtonVisibility(bool isVisible)
    {
        _waveButton.SetActive(isVisible);
    }

    public void SetDashButtonVisibility(bool isVisible)
    {
        _dashButton.SetActive(isVisible);
    }

    public void SetPickUpButtonVisibility(bool isVisible)
    {
        _pickUpButton.SetActive(isVisible);
    }

    public void SetOpenButtonVisibility(bool isVisible)
    {
        _openButton.SetActive(isVisible);
    }

    public void SetTimeRemaining(int timeRemaining)
    {
        _timer.timeRemaining = timeRemaining;
    }

    public void StartTimer()
    {
        _timerText.gameObject.SetActive(true);
        _timer.OnTimerPlay();
    }

    public int StopTimerAndGetTimeRemaining()
    {
        _timer.OnTimerPause();

        return Mathf.FloorToInt(_timer.timeRemaining);
    }

    public int GetLevelTimeTaken()
    {
        return _timer.GetTimeTaken();
    }

    private void Awake()
    {
        _dialogQueue = new Queue<string>();

        if (_mobileDetector.IsRunningOnMobile())
        {
            _mobileControls.SetActive(true);
        }
        else
        {
            _mobileControls.SetActive(false);
        }
    }

    private void OnEnable()
    {
        //Add subscription
        _playerInteract.OnInteracted += EndInteraction;
    }

    private void Update()
    {
        //Update timer text
        _timerText.text = _timer.GetTime();

        if (_timer.timeRemaining <= 60.0f)
        {
            _timerText.color = Color.red;
        }
    }

    private void EndInteraction()
    {
        EndDialog();
        SetPickUpButtonVisibility(false);
        SetOpenButtonVisibility(false);
    }
}
