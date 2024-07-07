using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void UpdateState() {
        if(!_player.MovementCompo.IsMove()) {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
