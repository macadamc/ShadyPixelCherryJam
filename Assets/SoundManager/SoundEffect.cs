using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Sound Effect")]
public class SoundEffect : ScriptableObject {

    public float minPitch;
    public float maxPitch;
    [Range(0,1)]
    public float volume;
    public AudioClip sfx;
}
