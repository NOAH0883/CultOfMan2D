using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialVisuals;
    [SerializeField] private InputActionProperty closeMenu;

    private int tutorialIndex = 0;
    private bool tutorialOpen;

    void Start()
    {
        tutorialOpen = true;
        GameData.tutorialDone = false;
        DontDestroyOnLoad(gameObject);

        tutorialIndex = 0;
        Time.timeScale = 0;

        // Enable action and subscribe ONCE here
        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;

        // Ensure only the first visual is active
        InitializeTutorialVisuals();
    }

    void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeTutorialVisuals()
    {
        for (int i = 0; i < tutorialVisuals.Count; i++)
        {
            if (tutorialVisuals[i] != null)
            {
                tutorialVisuals[i].SetActive(i == 0);
            }
        }
    }

    void OnCancel(InputAction.CallbackContext context)
    {
        // Close the current tutorial screen
        if (tutorialIndex >= 0 && tutorialIndex < tutorialVisuals.Count)
        {
            if (tutorialVisuals[tutorialIndex] != null)
            {
                tutorialVisuals[tutorialIndex].SetActive(false);
            }
        }

        tutorialOpen = false;
        Time.timeScale = 1;

        // If this was the last tutorial slide, finish up and destroy
        if (tutorialIndex >= tutorialVisuals.Count - 1)
        {
            GameData.tutorialDone = true;
            StartCoroutine(RemoveTutorial());
        }
    }


    IEnumerator RemoveTutorial()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    public void NextTutorial()
    {
        // Check if we can actually advance
        if (tutorialIndex + 1 >= tutorialVisuals.Count) return;

        Debug.Log("== New Tutorial Screen ==");
        Time.timeScale = 0;
        tutorialOpen = true;

        // Turn off current visual
        if (tutorialVisuals[tutorialIndex] != null)
        {
            tutorialVisuals[tutorialIndex].SetActive(false);
        }

        // Advance index and turn on next visual
        tutorialIndex++;

        if (tutorialVisuals[tutorialIndex] != null)
        {
            tutorialVisuals[tutorialIndex].SetActive(true);
        }

        // NO subscription here anymore! The input action is already listening.
    }

    private void OnDestroy()
    {
        // Safe unsubscription cleanup when object is destroyed
        if (closeMenu.action != null)
        {
            closeMenu.action.performed -= OnCancel;
        }
    }

    private void OnDisable()
    {
        if (closeMenu.action != null)
        {
            closeMenu.action.Disable();
        }
    }

    private void OnEnable()
    {
        if (closeMenu.action != null)
        {
            closeMenu.action.Enable();
        }
    }
}
