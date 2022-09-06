using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject chestOpen, chestClosed;

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("Player"))
        {
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
        }
    }
}
