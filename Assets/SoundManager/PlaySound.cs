using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public void Play(SoundEffect sound)
    {
        SoundManager.instance.PlaySoundEffect(sound);
    }
}
