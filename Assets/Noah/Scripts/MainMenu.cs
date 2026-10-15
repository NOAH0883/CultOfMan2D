using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject startButton;
    GameObject lastSelected;
    [SerializeField] GameObject mainStory;
    [SerializeField] InputActionProperty closeMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
        mainStory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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

    public void start()
    {
        mainStory.SetActive(true);
        startButton.SetActive(false);

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;

            SceneManager.LoadScene("Day1_tutorial 1");
        }

    }
}
