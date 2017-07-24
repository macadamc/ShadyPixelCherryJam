using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Random Move")]
public class RandomMoveAction : Action {

    public override void BeginAction(StateMachine stateMachine)
    {
        SetRandomMovePosition(stateMachine);
        if (stateMachine.monster == null)
            return;
        stateMachine.StopCoroutine(RandomMove(stateMachine));
        stateMachine.StartCoroutine(RandomMove(stateMachine));
    }

    public override void UpdateAction(StateMachine stateMachine)
    {

    }

    public override void EndAction(StateMachine stateMachine)
    {
        stateMachine.StopCoroutine(RandomMove(stateMachine));
    }

    public IEnumerator RandomMove(StateMachine stateMachine)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 20f));

        while (Vector2.Distance(stateMachine.transform.position, stateMachine.monster.targetPos) > 0.25f)
        {
            stateMachine.monster.MoveToTargetPosition(1f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 20f));
        SetRandomMovePosition(stateMachine);
        BeginAction(stateMachine);
    }

    public void SetRandomMovePosition(StateMachine stateMachine)
    {
        if (stateMachine.monster == null)
            return;

        Bounds bounds = GameObject.FindGameObjectWithTag("MoveBounds").GetComponent<BoxCollider2D>().bounds;
        float randomX = UnityEngine.Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
        float randomY = UnityEngine.Random.Range(bounds.center.y - bounds.extents.y, bounds.center.y + bounds.extents.y);
        Vector2 randomVector = new Vector2(randomX, randomY);
        stateMachine.monster.targetPos = randomVector;
    }

}
