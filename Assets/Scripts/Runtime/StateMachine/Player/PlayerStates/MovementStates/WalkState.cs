using UnityEngine;

public class WalkingState : PlayerMovementState
{
    public override void OnEnter()
    {
        Character.Animator.Play("Walk");
    }

    public override void OnUpdate()
    {
        if (Mathf.Abs(Character.MoveInput.x) < 0.1f)
            Machine.SetState(PlayerMovementStateKey.Idle);

        if (Time.time - Character.LastJumpInputTime <= Character.JumpInputBuffer && Character.CollisionInfo.m_below)
            Machine.SetState(PlayerMovementStateKey.Jumping);

        if (!Character.CollisionInfo.m_below && Character.Velocity.y < 0f)
            Machine.SetState(PlayerMovementStateKey.Falling);
    }
}