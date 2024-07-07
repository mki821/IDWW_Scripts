using UnityEngine;

public class PlayerCastState : PlayerState
{
    public PlayerCastState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Enter() {
        base.Enter();

        _player.moveable = false;
        _player.MovementCompo.StopImmediately();
    }

    public override void UpdateState() {
        _player.moveable = false;
    }

    public override void Exit() {
        _player.moveable = true;

        base.Exit();
    }
}
