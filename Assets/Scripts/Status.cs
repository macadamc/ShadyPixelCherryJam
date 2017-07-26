using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

    public Text moodText, hungerText, energyText;
    public StatusLevels moodLvl, hungerLvl, energyLvl;

    void Update()
    {
        if(GameStateManager.instance.loadedSave.hasMonster)
        {
            moodText.text = moodLvl.ReturnLevel(Mathf.Clamp (GameStateManager.instance.loadedSave.monsterInfo.mood,0,100));
            hungerText.text = hungerLvl.ReturnLevel(Mathf.Clamp(GameStateManager.instance.loadedSave.monsterInfo.hunger,0,100));
            energyText.text = energyLvl.ReturnLevel(Mathf.Clamp(GameStateManager.instance.loadedSave.monsterInfo.energy,0,100));
        }
    }
}
