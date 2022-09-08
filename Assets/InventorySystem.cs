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
        inventory.RemoveAt(itemID);
        InventoryUpdate();
    }
}
