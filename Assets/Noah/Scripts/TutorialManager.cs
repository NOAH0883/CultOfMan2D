using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{

     
    [SerializeField] List<GameObject> tutorialVisuals;
    int tutorialindex;

    [SerializeField] InputActionProperty closeMenu;

   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData.tutorialDone = false;
        DontDestroyOnLoad(gameObject);
        tutorialindex = 0;
        Time.timeScale = 0;

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
        tutorialVisuals[tutorialindex].SetActive(true);
        
    }

    
    //void Update()
    //{

    //    if (tutorialDone)
    //    {
    //        for (int i = 0; i < tutorialVisuals.Count; i++)
    //        {
    //            Destroy(tutorialVisuals[i]);
    //        }
    //    }
    //}

  

    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            if(tutorialindex >= 2)
            {
                GameData.tutorialDone = true;
                Destroy(gameObject);
            }

            tutorialVisuals[tutorialindex].SetActive(false);

            Time.timeScale = 1;
        }
    }


    public void nextTutorial()
    {
        Debug.Log("==New Tutorial==");
        Time.timeScale = 0;
        tutorialVisuals[tutorialindex].SetActive(false);
        tutorialindex++;
        tutorialVisuals[tutorialindex].SetActive(true);

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
    }
}
