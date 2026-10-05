using UnityEngine;

public class PlayerAttackStateMachine : StateMachine<PlayerAttackStateKey>
{
    public PlayerCharacter Character => m_character;
    private PlayerCharacter m_character;

    public PlayerAttackStateMachine(PlayerCharacter character)
    {
        m_character = character;
    }
}