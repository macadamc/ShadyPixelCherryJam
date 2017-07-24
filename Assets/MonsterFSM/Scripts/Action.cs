using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    public abstract void BeginAction(StateMachine stateMachine);
    public abstract void UpdateAction(StateMachine stateMachine);
    public abstract void EndAction(StateMachine stateMachine);



}
