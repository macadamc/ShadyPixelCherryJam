using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAdditive : MonoBehaviour {

    public void LoadScene(string name)
    {
        if(SceneManager.GetSceneByName(name).isLoaded == false)
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

}
