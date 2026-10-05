using UnityEngine;
using System;
using System.Collections.Generic;

public class StateMachine<KeyType> where KeyType : Enum
{
    private IState<KeyType> m_currentState;
    private readonly Dictionary<KeyType, IState<KeyType>> m_states = new();

    private KeyType m_currentKey;
    public KeyType CurrentKey => m_currentKey;

    public void RegisterState(KeyType key, IState<KeyType> state)
    {
        state.SetStateMachine(this);
        m_states.Add(key, state);
    }

    public void SetState(KeyType key)
    {
        m_currentState?.OnExit();
        m_currentState = m_states[key];
        m_currentKey = key;
        m_currentState.OnEnter();
    }

    public void Update()
    {
        m_currentState?.OnUpdate();
    }
}

public class Player
{
    private StateMachine<PlayerMovementStateKey> m_stateMachine;
}