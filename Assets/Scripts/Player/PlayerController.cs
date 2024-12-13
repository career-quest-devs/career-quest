using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        ChangeState(new PlayerDefaultState(this));
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
    }
}
