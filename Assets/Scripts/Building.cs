using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    public GameObject inventoryPanel;
    public GameObject InvItemPrefab;

    void Awake ()
    {
        foreach (string itemName in GameStateManager.instance.loadedSave.inventory)
        {
            ItemInfo info = Resources.Load("ItemInfo/" + itemName) as ItemInfo;

            GameObject item = Instantiate<GameObject>(InvItemPrefab);
            item.transform.SetParent(inventoryPanel.transform);
            item.GetComponent<Image>().sprite = info.image;
            item.transform.localScale = Vector2.one;
            item.GetComponent<InventoryItem>().info = info;
        }
    }

}
