using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] Health health;
    public int inventoryCapacity = 16;
    public GameObject[] inventorySpaces;
    public Sprite[] inventorySprites;
    public List<GameObject> inventory = new List<GameObject>();

    string objectName = "";

    public void InventoryUpdate()
    {
        inventorySpaces[inventory.Count].SetActive(false);

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySpaces[i].SetActive(true);
            objectName = inventory[i].name;
            switch (objectName)
            {
                case "apple":
                    inventorySpaces[i].GetComponent<Image>().sprite = inventorySprites[0];
                    break;
                case "avocado":
                    inventorySpaces[i].GetComponent<Image>().sprite = inventorySprites[1];
                    break;
                case "banana":
                    inventorySpaces[i].GetComponent<Image>().sprite = inventorySprites[2];
                    break;
            }
        }
    }

    public void GetClickedItem(int itemID)
    {
        string itemName = inventory[itemID].name;
        switch (itemName)
        {
            case "apple":
                health.health += 5;
                break;
            case "avocado":
                health.health += 10;
                break;
            case "banana":
                health.health += 15;
                break;
        }
        health.UpdateHealthSlider();
        inventory.RemoveAt(itemID);
        InventoryUpdate();
    }
}
