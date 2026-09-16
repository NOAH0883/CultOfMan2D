using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static IInteractable;
using static InputSystem_Actions;

public class CampFire : MonoBehaviour, Interactable
{

    [SerializeField] GameObject CampFireMenu;
    [SerializeField] InputActionProperty closeMenu;

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampFireMenu.SetActive(false);
    }

    public void Interact(VillageManager villageManager)
    {
       closeMenu.action.Enable();
       closeMenu.action.performed += OnCancel;

        CampFireMenu.SetActive(true);
        Time.timeScale = 0f;

    }

  
    void OnCancel (InputAction.CallbackContext context)  
    {
       if(context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            CampFireMenu.SetActive(false);
            Time.timeScale = 1f;

            
        }
            
    }



   

   





}
