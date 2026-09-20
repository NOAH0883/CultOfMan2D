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
    SceneLoader sceneLoader;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        huntingMenu.SetActive(false);

        sceneLoader = Object.FindAnyObjectByType<SceneLoader>();
        
    }

    public void Interact()
    {
        if (GameData.isDay)
        {

            //EventSystem.current.SetSelectedGameObject(null);
            //EventSystem.current.SetSelectedGameObject(firstButtonInMenu);

            closeMenu.action.Enable();
            closeMenu.action.performed += OnCancel;

            huntingMenu.SetActive(true);
            Time.timeScale = 0f;
        }
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
        closeMenu.action.Disable();
        closeMenu.action.performed -= OnCancel;


        huntingMenu.SetActive(false);
        Time.timeScale = 1f;

        string sceneToLoad = "GrassLands";
        Vector2 spawnPos = new Vector2(-7, 0);
        sceneLoader.LoadHunting(sceneToLoad, spawnPos);
    }
  





    //GameObject dayCycleMenu
    //dayCycleMenu.Setative(True)
    //rotation
    //when rotation is done
    //gameObject.setActive(False)
}
