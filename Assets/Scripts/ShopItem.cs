using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {

    public ItemInfo item;

    public void SetActiveShopItem()
    {
        FindObjectOfType<Shop>().SetShopSelected(item, gameObject);
    }
}
