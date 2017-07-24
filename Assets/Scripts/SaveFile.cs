using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if you change anything in here you have to also delete the save file. might be able to add things idk.
[System.Serializable]
public class SaveFile {

    public string lastExitTime;
    public bool hasMonster;
    public Monster.Info monsterInfo;

    public int dungeonUpgrade;
    public List<PlaceableObject.Info> dungeonObjects;
    public List<string> inventory;

    public SaveFile()
    {
        dungeonObjects = new List<PlaceableObject.Info>();
        inventory = new List<string>();
    }
}



[System.Serializable]
public class S_Vector2
{
    public float x;
    public float y;

    public S_Vector2() { }

    public S_Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(S_Vector2 intVector2)
    {
        return new Vector2(intVector2.x, intVector2.y);
    }

    public static implicit operator Vector3(S_Vector2 intVector2)
    {
        return new Vector3(intVector2.x, intVector2.y, 0);
    }
}