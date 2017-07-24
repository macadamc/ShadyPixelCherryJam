using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditorInternal;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager instance;

    public SaveFile loadedSave;

    void Awake()
    {
        if (GameStateManager.instance != null)
        {
            Destroy(this);
        }
        else
        {
            GameStateManager.instance = this;
        }

        LoadGame(Application.persistentDataPath + "/SaveFile.dat");
        Debug.Log(Application.persistentDataPath);
        

    }

    public void LoadGame(string path)
    {
        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            loadedSave = (SaveFile)formatter.Deserialize(fs);
            
        }
        else
        {
            Debug.Log("No save at path :" + " " + path);
        }
    }

    public void SaveGame(string path)
    {
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        BinaryFormatter formatter = new BinaryFormatter();

        loadedSave.lastExitTime = System.DateTime.Now.ToString();

        formatter.Serialize(fs, loadedSave);

        fs.Close();
    }

    void OnApplicationPause()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "TitleScreen")
        {
            SaveGame(Application.persistentDataPath + "/SaveFile.dat");
        }
        
    }

    void OnApplicationQuit()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "TitleScreen")
        {
            SaveGame(Application.persistentDataPath + "/SaveFile.dat");
        }
    }

    public void SetNewActiveMonster(MonsterType mon)
    {
        Monster.Info info = loadedSave.monsterInfo;
        List<string> randomTrait = new List<string>();
        randomTrait.Add(mon.traits[Random.Range(0, mon.traits.Count)].name);

        info = new Monster.Info(
                                mon.str, 
                                mon.def, 
                                mon.spd, 
                                mon.luck,
                                mon.growthStrMod,
                                mon.growthDefMod,
                                mon.growthSpdMod,
                                mon.growthLuckMod, 
                                mon.hungerMod, 
                                mon.energyMod, 
                                mon.moodMod, 
                                mon.statNegMoodThreshold, 
                                randomTrait
                                );

        loadedSave.hasMonster = true;
        loadedSave.monsterInfo = info;

        SaveGame(Application.persistentDataPath + "/SaveFile.dat");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive)
            return;

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Game")
        {
            /*
            TextBoxManager.instance.Display(string.Format("Welcome Back... lets see how {0} is doing, shall we?", loadedSave.monsterInfo.monsterName));
            TextBoxManager.instance.Display("Oh yeah!! i almost forgot....");
            TextBoxManager.instance.Display("if you move the 'slime' sprites around in the editor there positions are automaticly saved!! Woo!");
            */


            PlaceableObject.CreatePlaceableObjectsFromLoadedSave();


            PlaceableObject.Create("TestPlaceable", new S_Vector2(-5, 0), false); // this object is never saved beacuse it is never added to the dungeon!

            Monster monster = FindObjectOfType<Monster>();
            if(monster != null)
            {
                if (loadedSave.monsterInfo.currentState != null && loadedSave.monsterInfo.currentState != "")
                    monster.SetCurrentState(Resources.Load("Data/States/" + loadedSave.monsterInfo.currentState) as State);

                if (loadedSave.monsterInfo.currentAnimController != null && loadedSave.monsterInfo.currentAnimController != "")
                    monster.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Data/AnimatorControllers/" + loadedSave.monsterInfo.currentState) as RuntimeAnimatorController;
            }

            if (!loadedSave.hasMonster)
                OpenEggShop();
        }
    }

    void OpenEggShop()
    {
        SceneManager.LoadSceneAsync("PickEgg",LoadSceneMode.Additive);
    }
}
