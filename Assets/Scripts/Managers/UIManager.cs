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

    private void Awake()
    {
        _dialogQueue = new Queue<string>();
    }

    private void Update()
    {
        //Update timer text
        _timerText.text = _timer.GetTime();
    }
}
