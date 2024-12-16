using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTieAndSocksState : PlayerState
{
    public PlayerTieAndSocksState(PlayerController player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        _player.SetAppearance(_player.tieAndSocksSprite, _player.tieAndSocksAnimator);
        Debug.Log("Entered Player Tie And Socks State");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player.tieAndSocksSprite.SetActive(false);
    }
}
