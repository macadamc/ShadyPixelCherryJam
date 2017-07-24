using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSingle : MonoBehaviour {

    public void LoadScene(string name)
    {
        //SceneManager.LoadSceneAsync(name);
        StartCoroutine(FadeAndLoadScene(name, 1f, 0.5f));
    }

    public IEnumerator FadeAndLoadScene(string name, float time, float delayAfterFade)
    {
        TransitionManager.tm.Fade(1f, time);
        yield return new WaitForSeconds(time + delayAfterFade);
        SceneManager.LoadScene(name);
    }

}
