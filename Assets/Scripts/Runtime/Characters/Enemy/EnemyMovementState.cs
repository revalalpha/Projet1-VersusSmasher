using UnityEngine;

public abstract class EnemyMovementState : IState<EnemyMovementStateKey>
{
    protected EnemyMovementStateMachine Machine => m_machine;
    protected EnemyCharacter Character => m_machine.Character;

    private EnemyMovementStateMachine m_machine;

    public void SetStateMachine(StateMachine<EnemyMovementStateKey> stateMachine)
    {
        m_machine = (EnemyMovementStateMachine)stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}