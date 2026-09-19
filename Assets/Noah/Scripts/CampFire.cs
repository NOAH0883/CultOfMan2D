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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampFireMenu.SetActive(false);
    }

    public void Interact(VillageManager villageManager)
    {

        if (!open)
        {
            open = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonInMenu);


            closeMenu.action.Enable();
            closeMenu.action.performed += OnCancel;

            CampFireMenu.SetActive(true);
            Time.timeScale = 0f;
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

}
