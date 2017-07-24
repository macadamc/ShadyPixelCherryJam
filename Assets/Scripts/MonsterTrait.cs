using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class MonsterTrait : ScriptableObject
{
    [TextArea]
    public string description;

    //flat stat modifiers
    public float flatStrMod;
    public float flatDefMod;
    public float flatSpdMod;
    public float flatLuckMod;
    public float flatNegMoodThresholdMod;

    //stat modifiers when stat growth :p lol
    public float growthStrMod;
    public float growthDefMod;
    public float growthSpdMod;
    public float growthLuckMod;

    // you know
    public float hungerMod;
    public float energyMod;
    public float moodMod;
}