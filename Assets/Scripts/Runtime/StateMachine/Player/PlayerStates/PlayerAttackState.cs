using UnityEngine;

public abstract class PlayerAttackState : IState<PlayerAttackStateKey>
{
    protected PlayerAttackStateMachine m_machine;
    protected PlayerCharacter Character => m_machine.Character;

    private AttackData data;
    private float timer;

    public void SetStateMachine(StateMachine<PlayerAttackStateKey> stateMachine)
    {
        m_machine = (PlayerAttackStateMachine)stateMachine;
    }

    public virtual void OnEnter()
    {
        timer = 0f;

        if (m_machine.CurrentKey == PlayerAttackStateKey.None)
            return;

        data = AttackDatabase.Data[m_machine.CurrentKey];

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
        if (m_machine.CurrentKey == PlayerAttackStateKey.None)
            return;

        if (timer >= data.Duration)
            m_machine.SetState(PlayerAttackStateKey.None);
    }
}