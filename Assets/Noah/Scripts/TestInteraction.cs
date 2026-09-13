using UnityEngine;
using static IInteractable;

public class TestInteraction : MonoBehaviour, Interactable
{
   

    public void Interact()
    {
        Debug.Log("interacted");
    }
}
