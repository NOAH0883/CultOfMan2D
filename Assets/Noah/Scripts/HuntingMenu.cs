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
    [SerializeField] InputActionProperty closeMenu;
    SceneLoader sceneLoader;
    GameObject lastSelected;


    [Header("buttons")]
    [SerializeField] Button grasslandsButton;
    [SerializeField] GameObject grasslandsOverlay;
    [SerializeField] Button desertButton;
    [SerializeField] GameObject desertOverlay;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        huntingMenu.SetActive(false);

        sceneLoader = GetComponent<SceneLoader>();

        desertButton.enabled = false;
        desertOverlay.SetActive(true);

    }

    private void Update()
    {
        keepSelected();
        
    }

    public void Interact()
    {
        StartCoroutine(WaitFrame());

        ActiveButtons();

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;

        huntingMenu.SetActive(true);
        Time.timeScale = 0f;
    }


    IEnumerator WaitFrame()
    {
        yield return 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonInMenu.gameObject);
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

    public void GrassLands(string sceneToLoad)
    {
        if(GameData.isDay)
        {
            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            huntingMenu.SetActive(false);
            Time.timeScale = 1f;

            sceneLoader.LoadHunting(sceneToLoad);
        }
        
    }

    public void Desert(string sceneToLoad)
    {
        if (GameData.population >= 10 & GameData.isDay)
        {
            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            huntingMenu.SetActive(false);
            Time.timeScale = 1f;

            sceneLoader.LoadHunting(sceneToLoad);
        }

    }

    void ActiveButtons()
    {
        if (!GameData.isDay)
        {
            grasslandsButton.enabled = false;
            grasslandsOverlay.SetActive(true);

            desertButton.enabled = false;
            desertOverlay.SetActive(true);
        }
        else
        {
            grasslandsButton.enabled = true;
            grasslandsOverlay.SetActive(false);
        }
        if (GameData.isDay && GameData.population >= 10)
        {
            desertButton.enabled = true;
            desertOverlay.SetActive(false);
        }


    }


}
