using UnityEngine;

public class PlaceableObject : MonoBehaviour {

    public LayerMask itemLayer;

    // the class that gets saved to represent this object.
    [System.Serializable]
    public class Info
    {
        public string name;
        public S_Vector2 pos;

        public Info(string name, S_Vector2 pos)
        {
            this.name = name;
            this.pos = pos;
        }
    }

    //create a completely new placeable object and optionaly add it to the SaveFile. (think session state basicly, but its not realy tracked at all i guess..)
    public static PlaceableObject Create(string name, S_Vector2 pos, bool addToDungeon= true)
    {
        Info info = new Info(name, pos);

        if(addToDungeon)
        {
            GameStateManager.instance.loadedSave.dungeonObjects.Add(info);
        }

        GameObject go = Instantiate(Resources.Load("InGameObjects/"+info.name)) as GameObject;
        go.transform.position = info.pos;
        go.name = info.name;

        PlaceableObject behaviour = go.GetComponent<PlaceableObject>();
        behaviour.info = info;

        return behaviour;
    }

    //when the game is loaded this loops through all the objects in the savefile and creates there gameobjects.
    public static void CreatePlaceableObjectsFromLoadedSave()
    {
        foreach (Info info in GameStateManager.instance.loadedSave.dungeonObjects)
        {
            GameObject go = Instantiate(Resources.Load("InGameObjects/"+info.name)) as GameObject;
            go.transform.position = info.pos;
            go.name = info.name;

            PlaceableObject behaviour = go.GetComponent<PlaceableObject>();
            behaviour.info = info;
        }
    }

    public enum Type {None, Food, Cosmetic }

    public enum Placement {Floor, Wall}

    [HideInInspector]
    public Info info;

    public Type objectType;
    public Placement placement;

    public virtual void UpdateSaveInfo()
    {
        //position
        if (info.pos != transform.position)
        {
            info.pos.x = transform.position.x;
            info.pos.y = transform.position.y;
        }
    }

    public void LateUpdate()
    {
        UpdateSaveInfo();
    }

    void OnDestroy()
    {
        if (GameStateManager.instance.loadedSave.dungeonObjects.Contains(info))
            GameStateManager.instance.loadedSave.dungeonObjects.Remove(info);
    }

}
