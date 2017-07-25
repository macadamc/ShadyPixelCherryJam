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
            TextBoxManager.instance.Display("Your monster's egg has hatched!");
            TextBoxManager.instance.Display("Check out your new companion! ... And don't forget to take care of the little fellow!");
        }
    }

    public override void UpdateAction(StateMachine stateMachine)
    {
    }

    public override void EndAction(StateMachine stateMachine)
    {
    }

}
