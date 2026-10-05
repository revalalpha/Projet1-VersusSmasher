using UnityEngine;

public class EnemyWalkingState : EnemyMovementState
{
    public override void OnEnter()
    {
        Character.Animator.Play("Walk");
    }

    public override void OnUpdate()
    {
        if (Mathf.Abs(Character.MoveInput.x) < 0.1f)
            Machine.SetState(EnemyMovementStateKey.EnemyIdle);

        if (Time.time - Character.LastJumpInputTime <= Character.JumpInputBuffer && Character.CollisionInfo.m_below)
            Machine.SetState(EnemyMovementStateKey.EnemyJumping);

        if (!Character.CollisionInfo.m_below && Character.Velocity.y < 0f)
            Machine.SetState(EnemyMovementStateKey.EnemyFalling);
    }
}