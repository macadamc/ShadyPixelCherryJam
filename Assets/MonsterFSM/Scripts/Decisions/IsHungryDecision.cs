using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="FSM/Decisions/Is Hungry")]
public class IsHungryDecision : Decision {

    public float hungerThreshold;

    public override bool Decide(StateMachine stateMachine)
    {
        if (GameStateManager.instance.loadedSave.monsterInfo.hunger < hungerThreshold)
            return true;
        else
            return false;
    }
}
