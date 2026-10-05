using UnityEngine;

public abstract class PlayerMovementState : IState<PlayerMovementStateKey>
{
    protected PlayerMovementStateMachine Machine => m_machine;
    protected PlayerCharacter Character => m_machine.Character;

    private PlayerMovementStateMachine m_machine;

    public void SetStateMachine(StateMachine<PlayerMovementStateKey> stateMachine)
    {
        m_machine = (PlayerMovementStateMachine)stateMachine;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}