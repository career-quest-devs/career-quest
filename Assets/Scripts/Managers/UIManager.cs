using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialog UI")]
    public int dialogIndex;
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TMP_Text _dialogText;

    [Header("Timer UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer _timer;

    [Header("Mobile Controls")]
    [SerializeField] private GameObject _mobileControls;
    [SerializeField] private GameObject _sneezeButton;

    private Queue<string> _dialogQueue;

    public void StartDialogue(string[] dialogLines)
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

    private void Update()
    {
        //Update timer text
        _timerText.text = _timer.GetTime();
    }
}
