using System;
using UnityEngine;
using static IInteractable;

public class BerryBush : MonoBehaviour, Interactable
{
    [SerializeField] int foodAmount;
    public void Interact()
    {
        GameData.food += 1;

        Destroy(gameObject);
        
    }
}
