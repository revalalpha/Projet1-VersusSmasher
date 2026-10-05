using UnityEngine;

public abstract class EnemyAttackState : IState<EnemyAttackStateKey>
{
    protected EnemyAttackStateMachine m_machine;
    protected EnemyCharacter Character => m_machine.Character;

    private AttackData data;
    private float timer;

    public void SetStateMachine(StateMachine<EnemyAttackStateKey> stateMachine)
    {
        m_machine = (EnemyAttackStateMachine)stateMachine;
    }

    public virtual void OnEnter()
    {
        timer = 0f;

        if (m_machine.CurrentKey == EnemyAttackStateKey.None)
            return;

        data = AttackDatabase.EnemyData[m_machine.CurrentKey];

        Character.Animator.Play(data.AnimationName, 0);

        Character.Hitbox.Enable(data.Damage, data.KnockbackForce);
    }

    public virtual void OnExit()
    {
        Character.Hitbox.Disable();
    }

    public virtual void OnUpdate()
    {
        timer += Time.deltaTime;
        if (m_machine.CurrentKey == EnemyAttackStateKey.None)
            return;

        if (timer >= data.Duration)
            m_machine.SetState(EnemyAttackStateKey.None);
    }
}