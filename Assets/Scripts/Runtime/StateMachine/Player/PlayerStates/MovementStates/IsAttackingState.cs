using UnityEngine;

public class IsAttackingState : PlayerMovementState
{
    public override void OnEnter()
    {
        Character.Animator.SetBool("IsAttacking", true);
    }

    public override void OnUpdate()
    {
        Character.Move(Vector2.zero);

        if (Character.AttackMachine.CurrentKey == PlayerAttackStateKey.None)
        {
            Character.Animator.SetBool("IsAttacking", false);

            if (!Character.CollisionInfo.m_below)
                Machine.SetState(PlayerMovementStateKey.Falling);
            else if (Mathf.Abs(Character.MoveInput.x) > 0.1f)
                Machine.SetState(PlayerMovementStateKey.Walking);
            else
                Machine.SetState(PlayerMovementStateKey.Idle);
        }
    }
}