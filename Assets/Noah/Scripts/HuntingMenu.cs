using System.Collections;
using TMPro;
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
 
    SceneLoader sceneLoader;
    GameObject lastSelected;


    [Header("buttons")]
    [SerializeField] Button grasslandsButton;
    [SerializeField] GameObject grasslandsOverlay;
    [SerializeField] Button desertButton;
    [SerializeField] GameObject desertOverlay;


    InputSystem_Actions inputActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Disable();
        inputActions.UI.Disable();

        huntingMenu.SetActive(false);

        sceneLoader = GetComponent<SceneLoader>();

        desertButton.interactable = false;
        desertOverlay.SetActive(true);

    }

    private void Update()
    {
        if (inputActions.UI.Cancel.WasPressedThisFrame())
        {
            CloseMenu();
        }
    }

    public void Interact()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputActions.Player.Disable();

        inputActions.UI.Submit.Reset();


        ActiveButtons();
        
        huntingMenu.SetActive(true);
        Time.timeScale = 0f;


        StartCoroutine(WaitFrame());


    }


    IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();
        inputActions.UI.Enable();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonInMenu);

    }

    void keepSelected()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
        else if (lastSelected != null)
        {

            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }



    void CloseMenu()
    {
        inputActions.UI.Disable();
        inputActions.Player.Enable();

        huntingMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
    }

    public void GrassLands(string sceneToLoad)
    {
        if(GameData.isDay)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            huntingMenu.SetActive(false);
            Time.timeScale = 1f;

            sceneLoader.LoadHunting(sceneToLoad);
        }
        
    }

    public void Desert(string sceneToLoad)
    {
        if (GameData.population >= 7 & GameData.isDay)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            huntingMenu.SetActive(false);
            Time.timeScale = 1f;

            sceneLoader.LoadHunting(sceneToLoad);
        }

    }

    void ActiveButtons()
    {
        if (!GameData.isDay)
        {
            grasslandsButton.interactable = false;
            grasslandsOverlay.SetActive(true);

            desertButton.enabled = false;
            desertOverlay.SetActive(true);
        }
        else
        {
            grasslandsButton.interactable = true;
            grasslandsOverlay.SetActive(false);
        }
        if (GameData.isDay && GameData.population >= 7)
        {
            desertButton.interactable = true;
            desertOverlay.SetActive(false);
        }

    }


}
