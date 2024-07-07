using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Enter() {
        base.Enter();

        _player.AnimatorCompo.applyRootMotion = true;

        _player.moveable = false;
        _player.MovementCompo.StopImmediately();
    }
}
