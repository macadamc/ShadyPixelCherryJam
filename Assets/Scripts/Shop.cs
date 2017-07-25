using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject foodPanel;
    public GameObject itemsPanel;

    public Button buyButton;

    public GameObject shopItem;

    public Text infoBoxText;
    public List<ItemInfo> food;

    public GameObject selectBox;
    public ItemInfo selectedItem;
    public GameObject selectedTab;
	// Use this for initialization

	void Awake ()
    {
        foreach(ItemInfo f in food)
        {
            GameObject item = Instantiate(shopItem);
            item.GetComponent<Image>().sprite = f.image;
            item.transform.SetParent(foodPanel.transform, false);
            item.transform.localScale = Vector2.one;
            item.GetComponent<ShopItem>().item = f;
        }

        SetShopSelected();
	}

    public void SetShopSelected(ItemInfo item, GameObject gameObject)
    {
        selectBox.transform.position = gameObject.transform.position;
        selectedItem = item;
        infoBoxText.text = selectedItem.description;
        selectBox.SetActive(true);

        buyButton.interactable = true;
    }
    public void SetShopSelected()
    {
        selectBox.SetActive(false);
        selectedItem = null;
        infoBoxText.text = "Please select an item.";

        buyButton.interactable = false;
    }

    public void ChangeShopTab(GameObject panel)
    {
        if(foodPanel.activeInHierarchy && panel != foodPanel)
        {
            foodPanel.SetActive(false);
            SetShopSelected();
        }
        if (itemsPanel.activeInHierarchy && panel != itemsPanel)
        {
            itemsPanel.SetActive(false);
            SetShopSelected();
        }
        panel.SetActive(true);
    }



}
