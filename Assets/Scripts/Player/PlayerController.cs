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

    public void ChangeState(PlayerState newState)
    {
        _currentState?.OnStateExit();
        _currentState = newState;
        _currentState.OnStateEnter();
    }

    public void SetAppearance(GameObject sprite, Animator animator)
    {
        // Activate the desired sprite
        sprite.SetActive(true);

        //Set the current, active Animator
        currentAnimator = animator;
    }

    public void EnablePlayerInput()
    {
        _playerInput.enabled = true;
    }

    public void DisablePlayerInput()
    {
        _playerInput.enabled = false;
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        // Set initial PlayerState to default
        ChangeState(new PlayerDefaultState(this));
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
    }
}
