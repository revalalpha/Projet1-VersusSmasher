
using UnityEngine;

public class EnemyAttackStateMachine : StateMachine<EnemyAttackStateKey>
{
    public EnemyCharacter Character => m_character;
    private EnemyCharacter m_character;

    public EnemyAttackStateMachine(EnemyCharacter character)
    {
        m_character = character;
    }
}