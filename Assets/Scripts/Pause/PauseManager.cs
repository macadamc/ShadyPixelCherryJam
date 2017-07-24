using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public static PauseManager pm;

    public bool paused;
    public bool transitioning;

	// Use this for initialization
	void Awake () {

        if (pm == null)
            pm = this;
        else
        if (pm != this)
            Destroy(gameObject);
		
	}
}
