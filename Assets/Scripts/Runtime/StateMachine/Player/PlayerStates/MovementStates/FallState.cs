using UnityEngine;

public class FallingState : PlayerMovementState
{
    public override void OnEnter()
    {
        Character.Animator.Play("Falling");
    }

    public override void OnUpdate()
    {
        if (Character.CollisionInfo.m_below)
            Machine.SetState(PlayerMovementStateKey.Idle);
    }
}