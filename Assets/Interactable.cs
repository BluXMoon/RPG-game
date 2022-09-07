using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    InventorySystem inventorySystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventorySystem = other.GetComponent<InventorySystem>();
            if(inventorySystem.inventoryCapacity > inventorySystem.inventory.Count)
            {
                inventorySystem.inventory.Add(this.gameObject);
                inventorySystem.InventoryUpdate();
                this.gameObject.SetActive(false);
            }
        }
    }
}
