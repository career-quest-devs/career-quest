using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
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

    [Header("Player")]
    [SerializeField] private PlayerInteract _playerInteract;

    private Queue<string> _dialogQueue;

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

    public void StartTimer()
    {
        _timerText.gameObject.SetActive(true);
        _timer.OnTimerPlay();
    }

    private void Awake()
    {
        _dialogQueue = new Queue<string>();

        if (Application.platform == RuntimePlatform.Android)
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
        _playerInteract.OnInteracted += EndInteration;
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

    private void EndInteration()
    {
        EndDialog();
        SetPickUpButtonVisibility(false);
        SetOpenButtonVisibility(false);
    }
}
