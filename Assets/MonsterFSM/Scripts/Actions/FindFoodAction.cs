using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Find Food")]
public class FindFoodAction : Action {

    private Food curFood;

    public override void BeginAction(StateMachine stateMachine)
    {
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
        if (curFood == null)
        {
            FindRandomFood(stateMachine);
            return;
        }
        else
            stateMachine.monster.targetPos = curFood.gameObject.transform.position;

        if (Vector2.Distance(stateMachine.transform.position, curFood.gameObject.transform.position) < 0.6f)
        {
            curFood.Eat();
            curFood = null;
        }
        
    }

    public override void EndAction(StateMachine stateMachine)
    {
    }

    public void FindRandomFood(StateMachine stateMachine)
    {
        Food[] foods = FindObjectsOfType<Food>();

        if (foods.Length == 0)
            return;

        curFood = foods[UnityEngine.Random.Range(0, foods.Length)];
        stateMachine.monster.targetPos = curFood.gameObject.transform.position;
    }


}
