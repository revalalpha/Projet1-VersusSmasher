using UnityEngine;
using System;
using System.Collections.Generic;

public interface IState<KeyType> where KeyType : Enum 
{
    public void SetStateMachine(StateMachine<KeyType> stateMachine);
    public void OnEnter();
    public void OnExit();
    public void OnUpdate();
}