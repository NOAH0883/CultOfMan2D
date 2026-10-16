using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCOntroller : MonoBehaviour
{

    [SerializeField] GameObject tutorialVisual;
    [SerializeField] private InputActionProperty closeMenu;
    [SerializeField] bool tutorialEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        closeMenu.action.performed -= OnCancel;
        closeMenu.action.Disable();
        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;

        Debug.Log("=============================" + GameData.tutorialDone);
        if (GameData.tutorialDone)
        {
            //Time.timeScale = 1.0f;
            StartCoroutine(RemoveTutorial());
        }
        else
        {
            tutorialVisual.gameObject.SetActive(true);
            //Time.timeScale = 0f;
            
        }
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        Time.timeScale = 1.0f;

        tutorialVisual.gameObject.SetActive(false);
        StartCoroutine(RemoveTutorial());
    }


    IEnumerator RemoveTutorial()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        //Time.timeScale = 1.0f;
        Debug.Log("=====================Close tutorial=====================");
        if (closeMenu.action != null)
        {
            closeMenu.action.performed -= OnCancel;
            
        }
    }
}
