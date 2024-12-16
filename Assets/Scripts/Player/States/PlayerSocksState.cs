using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSocksState : PlayerState
{
    public PlayerSocksState(PlayerController player) : base(player)
    {

    }

    public override void OnStateEnter()
    {
        _player.SetAppearance(_player.socksSprite, _player.socksAnimator);
        Debug.Log("Entered Player Socks State");
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player.socksSprite.SetActive(false);
    }
}
