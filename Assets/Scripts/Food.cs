using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public ItemInfo foodItem;
    public bool dragged;

    public void Eat()
    {
        GameStateManager.instance.loadedSave.monsterInfo.hunger += foodItem.hungerGain;
        GameStateManager.instance.loadedSave.monsterInfo.energy += foodItem.energyGain;
        GameStateManager.instance.loadedSave.monsterInfo.mood += foodItem.moodGain;
        Destroy(gameObject);
    }
}
