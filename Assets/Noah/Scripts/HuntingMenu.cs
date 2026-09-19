using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static IInteractable;

public class HuntingMenu : MonoBehaviour, Interactable
{

    [SerializeField] GameObject huntingMenu;
    [SerializeField] GameObject firstButtonInMenu;
    [SerializeField] InputActionProperty closeMenu;
    [SerializeField] VillageManager villageManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        huntingMenu.SetActive(false);
    }

    public void Interact(VillageManager villageManager)
    {

        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(firstButtonInMenu);

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;

        huntingMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            huntingMenu.SetActive(false);
            Time.timeScale = 1f;

        }

    }

    public void GrassLands()
    {
        string sceneToLoad = "GrassLands";
        
        villageManager.LoadHunting(sceneToLoad);
    }
  





    //GameObject dayCycleMenu
    //dayCycleMenu.Setative(True)
    //rotation
    //when rotation is done
    //gameObject.setActive(False)
}
