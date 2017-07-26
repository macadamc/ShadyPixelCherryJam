using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnableAndDisable : MonoBehaviour {

    public SoundEffect startSound;
    public SoundEffect endSound;

	// Use this for initialization
	void Start () {

        SoundManager.instance.PlaySoundEffect(startSound);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {

        SoundManager.instance.PlaySoundEffect(endSound);
    }
}
