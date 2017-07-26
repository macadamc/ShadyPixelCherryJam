using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="FSM/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateMachine stateMachine)
    {
        UpdateActions(stateMachine);
        CheckTransitions(stateMachine);
    }

    public void BeginActions(StateMachine stateMachine)
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].BeginAction(stateMachine);
    }

    public void UpdateActions(StateMachine stateMachine)
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].UpdateAction(stateMachine);
    }

    public void EndActions(StateMachine stateMachine)
    {
        for (int i = 0; i < actions.Length; i++)
            actions[i].EndAction(stateMachine);
    }

    public void CheckTransitions(StateMachine stateMachine)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decision = true;

            foreach(Decision d in transitions[i].decisions)
            {

                if(d.Decide(stateMachine))
                {

                }
                else
                {
                    decision = false;
                }

            }

            if(decision == true)
            {
                if (transitions[i].trueState != null)
                    stateMachine.SetCurrentState(transitions[i].trueState);
            }
            else
            {
                if (transitions[i].falseState != null)
                    stateMachine.SetCurrentState(transitions[i].falseState);
            }
        }
    }
}
