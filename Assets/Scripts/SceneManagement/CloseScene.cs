using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseScene : MonoBehaviour {

    public void Close(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }
}
