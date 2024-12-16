using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTieState : PlayerState
{
    public PlayerTieState(PlayerController player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        _player.SetAppearance(_player.tieSprite, _player.tieAnimator);
        Debug.Log("Entered Player Tie State");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player.tieSprite.SetActive(false);
    }
}
