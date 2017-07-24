using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName ="FSM/Decisions/Test Age")]
public class TestAgeDecision : Decision {

    public float isPastAge;

    public override bool Decide(StateMachine stateMachine)
    {
        if (GameStateManager.instance.loadedSave.monsterInfo.age > isPastAge)
            return true;
        else
            return false;
    }
}
