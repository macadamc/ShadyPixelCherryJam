using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="FSM/Decisions/Has Food")]
public class HasFood : Decision {

    public override bool Decide(StateMachine stateMachine)
    {
        Food[] food = FindObjectsOfType<Food>();

        if (food.Length > 0)
            return true;
        else
            return false;
    }


}
