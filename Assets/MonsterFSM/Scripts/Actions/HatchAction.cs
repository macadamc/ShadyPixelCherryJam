using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Hatch")]
public class HatchAction : Action {

    public override void BeginAction(StateMachine stateMachine)
    {
        if(GameStateManager.instance.loadedSave.monsterInfo.hatched == false)
        {
            GameStateManager.instance.loadedSave.monsterInfo.hatched = true;
            TextBoxManager.instance.Display("The egg has hatched! Look at your new monster!");
        }
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
    }

    public override void EndAction(StateMachine stateMachine)
    {
    }

}
