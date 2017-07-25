using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Info")]
public class ItemInfo : ScriptableObject
{
    public int energyGain;
    public int hungerGain;
    public int moodGain;

    public int cost;

    public Sprite image;

    [TextArea]
    public string description;
}