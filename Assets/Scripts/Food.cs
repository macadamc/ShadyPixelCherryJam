using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Food Info")]
public class FoodPlaceableScriptableObject : ScriptableObject
{
    public int energyGain;
    public int hungerGain;
    public int moodGain;

    public int cost;
    [TextArea]
    public string description;
}

[RequireComponent(typeof(PlaceableObject))]
public class Food : MonoBehaviour
{
    public int energyGain;
    public int hungerGain;
    public int moodGain;

    public int cost;
    [TextArea]
    public string description;
}