using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void UpdateState() {
        if(_player.MovementCompo.IsMove()) {
            _stateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }
}
