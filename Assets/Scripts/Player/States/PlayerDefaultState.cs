using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : PlayerState
{
    public PlayerDefaultState(PlayerController player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        _player.SetAppearance(_player.defaultSprite, _player.defaultAnimator);
        Debug.Log("Entered Player Default State");
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        _player.defaultSprite.SetActive(false);
    }
}
