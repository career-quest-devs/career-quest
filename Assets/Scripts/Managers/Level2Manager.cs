using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;


    // Dialog collection
    private string[] _introDialog = new string[2]
    {
        "Alex: I got to be hurry.",
        "Alex: I remember the evalator is on the right."
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
