using System;
using UnityEngine;
using static IInteractable;

public class BerryBush : MonoBehaviour, Interactable
{
    [SerializeField] int foodAmount;
    public void Interact(VillageManager villageManager)
    {
        villageManager.currentFood += 1;

        Destroy(gameObject);
        
    }
}
