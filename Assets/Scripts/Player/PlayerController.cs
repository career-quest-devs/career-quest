using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject defaultSprite;
    public GameObject tieSprite;
    public GameObject socksSprite;
    public GameObject tieAndSocksSprite;

    public Animator defaultAnimator;
    public Animator tieAnimator;
    public Animator socksAnimator;
    public Animator tieAndSocksAnimator;
    public Animator currentAnimator;

    private PlayerState _currentState;
    private PlayerInput _playerInput;

    // Properties to track collected items
    public bool HasTie { get; private set; }
    public bool HasSocks { get; private set; }
    public bool HasResume { get; private set; }

    public void SetAppearance(GameObject sprite, Animator animator)
    {
        // Activate the desired sprite
        sprite.SetActive(true);

        //Set the current, active Animator
        currentAnimator = animator;
    }

    public void SwitchToPlayerActionMap()
    {
        _playerInput.SwitchCurrentActionMap("Player");
    }

    public void SwitchToUIActionMap()
    {
        _playerInput.SwitchCurrentActionMap("UI");
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        // Set initial PlayerState to default
        ChangeState(new PlayerDefaultState(this));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tie"))
        {
            HasTie = true;
            InitStateChange();
        }
        else if (collision.CompareTag("Socks"))
        {
            HasSocks = true;
            InitStateChange();
        }
        else if (collision.CompareTag("Resume"))
        {
            HasResume = true;
        }
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
    }

    private void InitStateChange()
    {
        if (HasTie && !HasSocks)
        {
            ChangeState(new PlayerTieState(this));
        }
        else if (HasSocks && !HasTie)
        {
            ChangeState(new PlayerSocksState(this));
        }
        else if (HasTie && HasSocks)
        {
            ChangeState(new PlayerTieAndSocksState(this));
        }
        else
        {
            ChangeState(new PlayerDefaultState(this));
        }
    }

    private void ChangeState(PlayerState newState)
    {
        _currentState?.OnStateExit();
        _currentState = newState;
        _currentState.OnStateEnter();
    }
}
