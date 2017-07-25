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
        List<string> randomTrait = new List<string>();
        randomTrait.Add(mon.traits[Random.Range(0, mon.traits.Count)].name);

        loadedSave.monsterInfo = new Monster.Info(
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

        FindObjectOfType<Monster>().SetCurrentState(mon.eggState);

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
            SceneManager.LoadSceneAsync("GameHUD", LoadSceneMode.Additive);

            PlaceableObject.CreatePlaceableObjectsFromLoadedSave();

            Monster monster = FindObjectOfType<Monster>();
            if(monster != null)
            {
                if (loadedSave.monsterInfo.currentState != null && loadedSave.monsterInfo.currentState != "")
                    monster.SetCurrentState(Resources.Load("Data/States/" + loadedSave.monsterInfo.currentState) as State);

                if (loadedSave.monsterInfo.currentAnimController != null)
                    monster.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("AnimatorControllers/" + loadedSave.monsterInfo.currentAnimController) as RuntimeAnimatorController;

                if(loadedSave.monsterInfo.hatched)
                {
                    Bounds bounds = GameObject.FindGameObjectWithTag("MoveBounds").GetComponent<BoxCollider2D>().bounds;
                    float randomX = UnityEngine.Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
                    float randomY = UnityEngine.Random.Range(bounds.center.y - bounds.extents.y, bounds.center.y + bounds.extents.y);
                    Vector2 randomVector = new Vector2(randomX, randomY);

                    monster.transform.position = randomVector;
                    monster.sprite.flipX = UnityEngine.Random.value > 0.5f ? true : false;
                }
            }

            if (!loadedSave.hasMonster)
            {
                StartCoroutine(OpenEggShopCoroutine());
            }
        }
    }

    public IEnumerator OpenEggShopCoroutine ()
    {
        //must yield first frame or ''GameObject.FindGameObjectWithTag("GameHUD")'' gets error
        yield return new WaitForEndOfFrame();
        GameObject hud = GameObject.FindGameObjectWithTag("GameHUD");
        hud.SetActive(false);

        yield return new WaitForSeconds(3f);
        TextBoxManager.instance.Display("Hey!");
        TextBoxManager.instance.Display("A new DUNGEON KEEPER?");
        TextBoxManager.instance.Display("It seems like you don't have a monster yet.");
        TextBoxManager.instance.Display("What kind of KEEPER doesn't have a monster?");
        TextBoxManager.instance.Display("Luckily, I can help you out!");
        yield return new WaitForSeconds(0.1f);
        yield return new WaitWhile(() => TextBoxManager.instance.isRunning);
        yield return new WaitForSeconds(.5f);

        SceneManager.LoadSceneAsync("PickEgg", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitWhile(() => SceneManager.GetSceneByName("PickEgg").isLoaded);
        yield return new WaitForSeconds(.5f);

        hud.SetActive(true);
    }
}
