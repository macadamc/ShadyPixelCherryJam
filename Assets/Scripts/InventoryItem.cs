using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : EventTrigger {
    Bounds wallSpace;
    Bounds floorSpace;

    public ItemInfo info;
    PlaceableObject inGameObject;
    Collider2D objCol;

    public RectTransform canvas;

    void Awake()
    {
        GameObject dungeon = GameObject.Find("Dungeon");
        floorSpace = dungeon.transform.Find("FloorSpace").gameObject.GetComponent<BoxCollider2D>().bounds;
        wallSpace = dungeon.transform.Find("WallSpace").gameObject.GetComponent<BoxCollider2D>().bounds;
        canvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<RectTransform>();

    }

    public static Vector2? lastSelectedPos;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        lastSelectedPos = transform.position;
        Vector3 newVector;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, transform.position, Camera.main, out newVector);
        inGameObject = PlaceableObject.Create(info.name, new S_Vector2(newVector.x, newVector.y), true);
        objCol = inGameObject.GetComponent<Collider2D>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Vector3 newVector;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, transform.position, Camera.main, out newVector);
        inGameObject.transform.position = newVector;

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector3 newVector;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas, transform.position, Camera.main, out newVector);

        if (inGameObject.placement == PlaceableObject.Placement.Floor)
        {
            //floor check
            if(floorSpace.Contains(newVector))
            {
                if(Physics2D.IsTouchingLayers(objCol, inGameObject.itemLayer))
                {
                    //if touching objects in layer
                    transform.position = (Vector2)lastSelectedPos;
                    lastSelectedPos = null;
                    Destroy(inGameObject.gameObject);
                }
                else
                {
                    //if valid position
                    GameStateManager.instance.loadedSave.inventory.Remove(info.name);
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = (Vector2)lastSelectedPos;
                lastSelectedPos = null;
                Destroy(inGameObject.gameObject);
            }
        }
        else
        if (inGameObject.placement == PlaceableObject.Placement.Wall)
        {
            //wall check
            if (wallSpace.Contains(newVector))
            {
                if (Physics2D.IsTouchingLayers(objCol, inGameObject.itemLayer))
                {
                    //if touching objects in layer
                    transform.position = (Vector2)lastSelectedPos;
                    lastSelectedPos = null;
                    Destroy(inGameObject.gameObject);
                }
                else
                {
                    //if valid position
                    GameStateManager.instance.loadedSave.inventory.Remove(info.name);
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = (Vector2)lastSelectedPos;
                lastSelectedPos = null;
                Destroy(inGameObject.gameObject);
            }
        }
    }

    
}
