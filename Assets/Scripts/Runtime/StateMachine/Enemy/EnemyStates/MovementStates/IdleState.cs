using UnityEngine;

public class EnemyIdleState : EnemyMovementState
{
    public override void OnEnter()
    {
        Character.Animator.Play("Idle");
    }

    public override void OnUpdate()
    {
        if (Mathf.Abs(Character.MoveInput.x) > 0.1f)
            Machine.SetState(EnemyMovementStateKey.EnemyWalking);

        if (Time.time - Character.LastJumpInputTime <= Character.JumpInputBuffer && Character.CollisionInfo.m_below)
            Machine.SetState(EnemyMovementStateKey.EnemyJumping);

        if (!Character.CollisionInfo.m_below && Character.Velocity.y < 0f)
            Machine.SetState(EnemyMovementStateKey.EnemyFalling);
    }
}