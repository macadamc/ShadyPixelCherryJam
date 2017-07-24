using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MonsterType : ScriptableObject
{
    public List<MonsterTrait> traits;

    public float str;
    public float def;
    public float spd;
    public float luck;

    public float growthStrMod;
    public float growthDefMod;
    public float growthSpdMod;
    public float growthLuckMod;

    public float hungerMod = 1f;
    public float energyMod = 1f;
    public float moodMod = 1f;

    public float statNegMoodThreshold = 25f;
}
