using UnityEngine;

public class EnemyIsAttackingState : EnemyMovementState
{
    public override void OnEnter()
    {
        Character.Animator.SetBool("IsAttacking", true);
    }

    public override void OnUpdate()
    {
        Character.Move(Vector2.zero);

        if (Character.AttackMachine.CurrentKey == EnemyAttackStateKey.None)
        {
            Character.Animator.SetBool("IsAttacking", false);

            if (!Character.CollisionInfo.m_below)
                Machine.SetState(EnemyMovementStateKey.EnemyFalling);
            else if (Mathf.Abs(Character.MoveInput.x) > 0.1f)
                Machine.SetState(EnemyMovementStateKey.EnemyWalking);
            else
                Machine.SetState(EnemyMovementStateKey.EnemyIdle);
        }
    }
}