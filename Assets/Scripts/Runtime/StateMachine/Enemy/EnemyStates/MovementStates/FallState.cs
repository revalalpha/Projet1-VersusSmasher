using UnityEngine;

public class EnemyFallingState : EnemyMovementState
{
    public override void OnEnter()
    {
        Character.Animator.Play("Falling");
    }

    public override void OnUpdate()
    {
        if (Character.CollisionInfo.m_below)
            Machine.SetState(EnemyMovementStateKey.EnemyIdle);
    }
}