using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Move To Target Position")]
public class MoveToTargetPositionAction : Action {

    public override void BeginAction(StateMachine stateMachine)
    {
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
        if(Vector2.Distance(stateMachine.transform.position,stateMachine.monster.targetPos) > 0.5f)
            stateMachine.monster.MoveToTargetPosition(1f);
    }

    public override void EndAction(StateMachine stateMachine)
    {

    }


}
