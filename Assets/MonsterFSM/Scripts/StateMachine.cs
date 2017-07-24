using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine : MonoBehaviour
{
    public State startState;
    public State currentState;
    public State lastState;

    public Monster monster;

    public virtual void Awake()
    {
        monster = GetComponent<Monster>();
    }

    public void Update()
    {
        if (currentState == null)
            return;

        currentState.UpdateState(this);
    }

    public void SetCurrentState(State state)
    {
        if (state == null)
            return;

        if(currentState != null)
        {
            currentState.EndActions(this);
            lastState = currentState;
        }
        currentState = state;
        currentState.BeginActions(this);
    }
}
