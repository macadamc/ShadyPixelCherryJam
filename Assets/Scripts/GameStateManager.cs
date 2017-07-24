using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

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

        if (scene.name == "Game")
        {
            PlaceableObject.CreatePlaceableObjectsFromLoadedSave();

            if (!loadedSave.hasMonster)
                //OpenEggShop();
                StartCoroutine(OpenEggShopWithSomeText());
            else
            {
                TextBoxManager.instance.Display("Welcome Back.");
            }
        }
    }

    void OpenEggShop()
    {
        SceneManager.LoadSceneAsync("PickEgg",LoadSceneMode.Additive);
    }

    IEnumerator OpenEggShopWithSomeText()
    {
        TextBoxManager.instance.Display("Lets Go Get our First Egg!!");
        yield return null;
        //yield return new WaitForSeconds(0);// we need to wait until the NEXT frame for IsRunning to be set..
        yield return new WaitWhile(() => TextBoxManager.instance.isRunning);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadSceneAsync("PickEgg", LoadSceneMode.Additive);
    }
}
