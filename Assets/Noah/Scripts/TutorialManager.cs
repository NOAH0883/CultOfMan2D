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

    public bool tutorialDone;


    [SerializeField] GameObject campFireArrow;
    [SerializeField] GameObject huntingArrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        tutorialindex = 0;

        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
        tutorialVisuals[tutorialindex].SetActive(true);
        tutorialDone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(GameData.isDay)
        {
            huntingArrow.SetActive(true);
            campFireArrow.SetActive(false);
        }
        else
        {
            huntingArrow.SetActive(false);
            campFireArrow.SetActive(true);
        }
        
       

        if (tutorialDone)
        {
            for (int i = 0; i < tutorialVisuals.Count; i++)
            {
                Destroy(tutorialVisuals[i]);
            }
        }
    }

  

    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            tutorialVisuals[tutorialindex].SetActive(false);

            if (tutorialindex >= tutorialVisuals.Count)
            {
                tutorialDone = true;
            }

        }

    }


    public void nextTutorial()
    {
        Debug.Log("==New Tutorial==");

       

        if (tutorialindex < 3)
        {
            tutorialVisuals[tutorialindex].SetActive(false);
            tutorialindex++;
            tutorialVisuals[tutorialindex].SetActive(true);
        }
        else
        {
            tutorialDone = true;
        }


            closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;
    }
}
