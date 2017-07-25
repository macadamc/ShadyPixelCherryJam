using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(GameStateManager))]
public class SaveDataEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Delete Save File."))
        {
            string path = Application.persistentDataPath + "/SaveFile.dat";
            if ( File.Exists(path) )
            {
                File.Delete(path);
                Debug.Log("Save File Deleted.");
            }

        }
    }
}
