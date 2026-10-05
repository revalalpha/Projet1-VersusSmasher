
using UnityEngine;

public class EnemyMovementStateMachine : StateMachine<EnemyMovementStateKey>
{
    public EnemyCharacter Character => m_character;
    private EnemyCharacter m_character;

    public EnemyMovementStateMachine(EnemyCharacter character)
    {
        m_character = character;
    }
}