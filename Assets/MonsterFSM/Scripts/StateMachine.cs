using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State startState;
    public State currentState;
    public State lastState;

    public void Start()
    {
        if (currentState == null)
            SetCurrentState(startState);
    }

    public void Update()
    {
        if (currentState == null)
            return;

        currentState.UpdateState(this);
    }

    public void SetCurrentState(State state)
    {
        if(currentState != null)
        {
            currentState.EndActions(this);
            lastState = currentState;
        }
        currentState = state;
        currentState.BeginActions(this);
    }
}
