using UnityEngine;

public class PlayerMovementStateMachine : StateMachine<PlayerMovementStateKey>
{
    public PlayerCharacter Character => m_character;
    private PlayerCharacter m_character;

    public PlayerMovementStateMachine(PlayerCharacter character)
    {
        m_character = character;
    }
}