using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using static IInteractable;
using static InputSystem_Actions;

public class CampFire : MonoBehaviour, Interactable
{

    [SerializeField] GameObject CampFireMenu;
    [SerializeField] InputActionProperty closeMenu;
    [SerializeField] GameObject firstButtonInMenu;
    bool open;

    VillageManager villageManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampFireMenu.SetActive(false);

        villageManager = UnityEngine.Object.FindAnyObjectByType<VillageManager>();
    }

    public void Interact()
    {

        if (!GameData.isDay)
        {
            if (!open)
            {
                //open = true;
                //EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(firstButtonInMenu);


                closeMenu.action.Enable();
                closeMenu.action.performed += OnCancel;

                CampFireMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

  
    void OnCancel (InputAction.CallbackContext context)  
    {
       if(context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            CampFireMenu.SetActive(false);
            Time.timeScale = 1f;
            open = false;
        }
            
    }


    public void Feed()
    {

        if (!GameData.hasFeedVillage)
        {
            Debug.Log("FeedVIllages");
            GameData.hasFeedVillage = true;
            villageManager.Test();
        }
            
    }

    public void Sacrifice()
    {
        if(GameData.population > 1 )
            villageManager.Sacrifice();
    }

    public void UpgradeWeapon()
    {
        if(GameData.food>3 && !villageManager.hasWeapon)
        {
            villageManager.hasWeapon = true;
            Debug.Log("Give playerWeapon");
            GameData.food -= 3;
        }
        else
        {
            Debug.Log("Not enough food");
        }
    }
        


}
