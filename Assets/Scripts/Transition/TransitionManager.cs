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

        transitionImage.enabled = true;
        transitionImage.CrossFadeAlpha(1f, 0f, true);
        transitionImage.canvasRenderer.SetAlpha(1f);
        Invoke("FadeIn", 0.5f);
    }

    void FadeIn()
    {
        transitionImage.enabled = true;
        transitionImage.CrossFadeAlpha(1f, 0f, true);
        transitionImage.canvasRenderer.SetAlpha(1f);
        transitionImage.CrossFadeAlpha(0f, 1f, true);
    }

    public void Fade(float alpha, float time)
    {
        transitionImage.enabled = true;
        Pause();
        transitionImage.CrossFadeAlpha(0f, 0f, true);
        transitionImage.canvasRenderer.SetAlpha(0.0f);
        transitionImage.CrossFadeAlpha(alpha, time, true);
    }

    void UnPause()
    {
        PauseManager.pm.transitioning = false;
        transitionImage.gameObject.SetActive(false);
        
    }

    void Pause()
    {
        transitionImage.gameObject.SetActive(true);
        PauseManager.pm.transitioning = true;
    }
}
