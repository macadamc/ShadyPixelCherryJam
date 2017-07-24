using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Set Animator Controller")]
public class SetAnimatorControllerAction : Action {

    public RuntimeAnimatorController animationController;

    public override void BeginAction(StateMachine stateMachine)
    {
        FindObjectOfType<Monster>().GetComponent<Animator>().runtimeAnimatorController = animationController;
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
    }

    public override void EndAction(StateMachine stateMachine)
    {
    }

}
