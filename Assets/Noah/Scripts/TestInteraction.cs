using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static IInteractable;

public class TestInteraction : MonoBehaviour, Interactable
{
    VillageManager villageManager;
    [SerializeField] GameObject CampFireMenu;
    [SerializeField] InputActionProperty closeMenu;
    public void Interact()
    {
        if (!GameData.isDay)
        {
            Debug.Log("I have interacted");
            
            villageManager = UnityEngine.Object.FindAnyObjectByType<VillageManager>();
            closeMenu.action.Enable();
            closeMenu.action.performed += OnCancel;

            CampFireMenu.SetActive(true);
            Time.timeScale = 0f;
        }


    }


    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            CampFireMenu.SetActive(false);
            Time.timeScale = 1f;

        }

    }




    public void Feed()
    {

        if (!GameData.hasFeedVillage)
        {
            GameData.hasFeedVillage = true;
            villageManager.Test();
        }

    }

    public void Sacrifice()
    {
        if (GameData.population > 1)
            villageManager.Sacrifice();
    }

    //public void UpgradeWeapon()
    //{
    //    if (GameData.food > 3 && !villageManager.hasWeapon)
    //    {
    //        villageManager.hasWeapon = true;
    //        Debug.Log("Give playerWeapon");
    //        GameData.food -= 3;
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough food");
    //    }
    //}
}
