using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(PauseManager))]
public class TransitionManager : MonoBehaviour {

    public static TransitionManager tm;

    public Image transitionImage;

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

	// Use this for initialization
	void Awake () {

        if (tm == null)
            tm = this;
        else
        if (tm != this)
            Destroy(gameObject);

	}
	
	// Update is called once per frame
	void Update () {


		
	}

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive)
            return;
        if(Time.time > 1)
        {
            Debug.Log("scene loaded");
            FadeIn();
        }
    }

    void FadeIn()
    {
        transitionImage.CrossFadeAlpha(1f, 0f, true);
        transitionImage.canvasRenderer.SetAlpha(1f);
        transitionImage.CrossFadeAlpha(0f, 1f, true);
    }

    public void Fade(float alpha, float time)
    {
        Pause();
        transitionImage.CrossFadeAlpha(0f, 0f, true);
        transitionImage.canvasRenderer.SetAlpha(0.0f);
        transitionImage.CrossFadeAlpha(alpha, time, true);
    }

    void UnPause()
    {
        PauseManager.pm.transitioning = false;
        transitionImage.enabled = false;
    }

    void Pause()
    {
        transitionImage.enabled = true;
        PauseManager.pm.transitioning = true;
    }
}
