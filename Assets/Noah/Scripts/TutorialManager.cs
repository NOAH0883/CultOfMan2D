using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{

     
    [SerializeField] List<GameObject> tutorialVisuals;
    int tutorialindex;

    [SerializeField] InputActionProperty closeMenu;

    bool menuOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        tutorialindex = 0;

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
        tutorialVisuals[tutorialindex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (tutorialindex >= tutorialVisuals.Count)
        {
            Destroy(gameObject);
        }


        
    }

  

    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            tutorialVisuals[tutorialindex].SetActive(false);
            Time.timeScale = 1f;
        }

    }


    public void nextTutorial()
    {
        if (tutorialindex >= tutorialVisuals.Count)
        {
            Destroy(gameObject);
        }

        tutorialVisuals[tutorialindex].SetActive(false);
        tutorialindex++;
        tutorialVisuals[tutorialindex].SetActive(true);

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
        

        Time.timeScale = 1f;
    }
}
