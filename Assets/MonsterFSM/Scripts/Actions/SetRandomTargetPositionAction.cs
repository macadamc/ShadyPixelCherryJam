using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Set Random Target Position")]
public class SetRandomTargetPositionAction : Action {

    public override void BeginAction(StateMachine stateMachine)
    {
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
        if (Vector2.Distance(stateMachine.transform.position, stateMachine.monster.targetPos) < 0.5f)
        {
            Bounds bounds = GameObject.FindGameObjectWithTag("MoveBounds").GetComponent<BoxCollider2D>().bounds;
            float randomX = UnityEngine.Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
            float randomY = UnityEngine.Random.Range(bounds.center.y - bounds.extents.y, bounds.center.y + bounds.extents.y);
            Vector2 randomVector = new Vector2(randomX, randomY);
            stateMachine.monster.targetPos = randomVector;
        }
    }

    public override void EndAction(StateMachine stateMachine)
    {
    }


}
