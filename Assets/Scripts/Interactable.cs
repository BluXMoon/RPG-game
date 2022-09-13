using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, ISaveable
{
    GameObject player;
    InventorySystem inventorySystem;
    public bool collected;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventorySystem = player.GetComponent<InventorySystem>();

        if (collected) 
        {
            AddToInventory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(inventorySystem.inventoryCapacity > inventorySystem.inventory.Count)
            {
                collected = true;
                CaptureState();
                AddToInventory();
            }
        }
    }

    private void AddToInventory()
    {
        inventorySystem.inventory.Add(this.gameObject);
        inventorySystem.InventoryUpdate();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public object CaptureState()
    {
        return collected;
    }

    public void RestoreState(object state)
    {
        collected = (bool)state;
    }
}
