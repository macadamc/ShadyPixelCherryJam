using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

    public static TimeManager tm;

    public delegate void UpdateObjects(float time);
    public UpdateObjects updateObjects;

	// Use this for initialization
	void Awake () {

        if (tm == null)
            tm = this;
        else
        if (tm != this)
            Destroy(gameObject);

        SceneManager.sceneLoaded += UpdateTime;
	}

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= UpdateTime;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateTime;
    }

    public void UpdateTime(Scene scene, LoadSceneMode sceneMode)
    {
        if (sceneMode == LoadSceneMode.Additive || scene.name == "TitleScreen")
            return;


        if (String.IsNullOrEmpty(GameStateManager.instance.loadedSave.lastExitTime))
        {
            Debug.Log("LastExitTime is nullOrEmpty");
            return;
        }
        DateTime curTime = System.DateTime.Now;
        DateTime oldTime = DateTime.Parse(GameStateManager.instance.loadedSave.lastExitTime);

        TimeSpan timePassed = curTime.Subtract(oldTime);
        Debug.Log(timePassed.TotalSeconds);

        if (updateObjects == null)
            return;
        updateObjects((float)timePassed.TotalSeconds);
    }

    void Update()
    {
        if (updateObjects == null)
            return;
        updateObjects(Time.deltaTime);
    }
}
