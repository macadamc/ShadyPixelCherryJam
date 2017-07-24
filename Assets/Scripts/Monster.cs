using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour {

    [System.Serializable]
    public class Info
    {
        public string monsterName;
        public List<string> traits;
        public bool hatched;

        [Range(0f, 100f)]
        public float hunger = 75f;
        [Range(0f, 100f)]
        public float energy = 75f;
        [Range(0f, 100f)]
        public float mood = 75f;

        public float str;
        public float def;
        public float spd;
        public float luck;

        public float age;

        public float growthStrMod;
        public float growthDefMod;
        public float growthSpdMod;
        public float growthLuckMod;

        public float hungerMod = 1f;
        public float energyMod = 1f;
        public float moodMod = 1f;

        public float statNegMoodThreshold = 25f;

        public Info(float _str, float _def, float _spd, float _luck, float _strGrowth, float _defGrowth, float _spdGrowth, float _luckGrowth, float _hungerMod, float _energyMod, float _moodMod, float _statNegThresh, List<string> _traits)
        {
            str = _str;
            def = _def;
            spd = _spd;
            luck = _luck;
            growthStrMod = _strGrowth;
            growthDefMod = _defGrowth;
            growthSpdMod = _spdGrowth;
            growthLuckMod = _luckGrowth;
            hungerMod = _hungerMod;
            energyMod = _energyMod;
            moodMod = _moodMod;
            statNegMoodThreshold = _statNegThresh;
            traits = _traits;
        }
    }

    public Info info;

    void Awake()
    {
        TimeManager.tm.updateObjects += UpdateMonster;
        info = GameStateManager.instance.loadedSave.monsterInfo;
    }

    void OnDisable()
    {
        TimeManager.tm.updateObjects -= UpdateMonster;
    }

    void OnDestroy()
    {
        TimeManager.tm.updateObjects -= UpdateMonster;
    }

    public void UpdateMonster(float timetoUpdate)
    {
        info.age += timetoUpdate;

        float newUpdateTime = timetoUpdate;

        if (info.hatched == false)
            return;

        if (timetoUpdate > 1f)
        {
            newUpdateTime = Mathf.FloorToInt(timetoUpdate);

            for (int i = 0; i < newUpdateTime; i++)
            {
                info.hunger -= info.hungerMod;
                info.energy -= info.energyMod;
                if (info.hunger < info.statNegMoodThreshold || info.energy < info.statNegMoodThreshold)
                {
                    info.mood -= info.moodMod;
                }
            }
        }
        else
        {
            info.hunger -= timetoUpdate * info.hungerMod;
            info.energy -= timetoUpdate * info.energyMod;
            if (info.hunger < info.statNegMoodThreshold || info.energy < info.statNegMoodThreshold)
            {
                info.mood -= timetoUpdate * info.moodMod;
            }
        }
    }

    public void AddTrait(string traitName)
    {
        //MonsterTrait monsterTrait = Resources.Load("/MonsterTraits" + traitName) as MonsterTrait;

        if (info.traits.Contains(traitName) == false)
        {
            info.traits.Add(traitName);
        }
    }

    public void RemoveTrait(string trait)
    {
        if (info.traits.Contains(trait))
        {
            info.traits.Remove(trait);
        }
    }
}
