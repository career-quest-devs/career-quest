using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private TMP_Text _dialogBox;
    public int dialogIndex;
    [SerializeField] private DictionaryDialogs _dialogs;

    [Header("Timer UI")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Timer _timer;

    public void OnDisplayDialog()
    {
        DisplayDialog(_dialogs.GetTestText(dialogIndex));
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer text
        _timerText.text = _timer.GetTime();
    }

    void DisplayDialog(string dialog)
    {
        _dialogBox.text = dialog;
    }
}
