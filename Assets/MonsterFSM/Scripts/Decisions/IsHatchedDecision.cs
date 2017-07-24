using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName ="FSM/Decisions/Is Hatched")]
public class IsHatchedDecision : Decision {

    public override bool Decide(StateMachine stateMachine)
    {
        return GameStateManager.instance.loadedSave.monsterInfo.hatched;
    }
}
