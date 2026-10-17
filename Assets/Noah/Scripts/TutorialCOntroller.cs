using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCOntroller : MonoBehaviour
{

    [SerializeField] GameObject tutorialVisual;
   
    [SerializeField] bool tutorialEnd;

    [SerializeField] bool isCollider;
    InputSystem_Actions inputActions;


   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
        if(!isCollider)
        {
            if (GameData.tutorialDone)
            {
                StartCoroutine(RemoveTutorial());
            }
            else
            {
                inputActions.Player.Disable();
                inputActions.UI.Enable();


                tutorialVisual.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        
       
    }


    private void Update()
    {
        if (inputActions.UI.Cancel.WasPressedThisFrame())
        {
            if (tutorialEnd)
            {
                GameData.tutorialDone = true;
            }

            tutorialVisual.gameObject.SetActive(false);
            StartCoroutine(RemoveTutorial());
        }
    }


    IEnumerator RemoveTutorial()
    {
        Time.timeScale = 1f;

        inputActions.Player.Enable();
        inputActions.UI.Disable();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !GameData.isDay)
        {
            inputActions.Player.Disable();
            inputActions.UI.Enable();

            tutorialVisual.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
       
    }


}
