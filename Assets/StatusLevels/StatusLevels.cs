using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class StatusLevels : ScriptableObject {

    [System.Serializable]
    public class Level
    {
        public string name;
        public float min;
        public float max;
    }

    public Level[] levels;

    public string ReturnLevel(float value)
    {
        foreach(Level level in levels)
        {
            if(value >= level.min && value <= level.max)
            {
                return level.name;
            }
        }
        return "???";
    }
}
