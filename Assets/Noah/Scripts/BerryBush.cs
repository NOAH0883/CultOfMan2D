using System;
using UnityEngine;
using static IInteractable;

public class BerryBush : MonoBehaviour, Interactable
{
    [SerializeField] int foodAmount;
    public void Interact()
    {
        Debug.Log("Yay picked up carrot");
        GameData.food += foodAmount;

        Destroy(gameObject);
        
    }
}
