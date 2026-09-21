using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DayCycle : MonoBehaviour
{
    [SerializeField] GameObject dayCycleMenu;
    [SerializeField] float rotationSpeed = 2f; // Time in seconds the rotation should take\

    // tutorial menu 
    [SerializeField] GameObject tutorialMenu;

 


    bool hasRotated;

    void Start()
    {
        

        DayCycleOn();
        hasRotated = false;
        
        //tutorialMenu.SetActive(false);
    }


    void Update()
    {
        

        //check if has rotated
        if(hasRotated && tutorialMenu != null)
            tutorial();
    }

    IEnumerator Rotate()
    {
        hasRotated = false;
        dayCycleMenu.SetActive(true);

        // 1. Store the exact starting rotation before the loop begins
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);

        float timeElapsed = 0f;

        // 2. Loop strictly based on time matching your rotationSpeed duration
        while (timeElapsed < rotationSpeed)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / rotationSpeed;

            // 3. FIX: Lerp between the fixed START rotation and END rotation
            dayCycleMenu.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        // Ensure it snaps perfectly to the target at the end
        transform.rotation = endRotation;

        // This will now trigger successfully!
        Debug.Log("wait");

        yield return new WaitForSeconds(2f);

        dayCycleMenu.SetActive(false);

        yield return new WaitForSeconds(.5f);

        tutorial();
        hasRotated = true;
        
        // turn on tutorial 
    }

    public void DayCycleOn()
    {
        StartCoroutine(Rotate());
    }

    void tutorial()
    {
        tutorialMenu.SetActive(true);
        Time.timeScale = 0;
        
        //turn off tutorial 
        //destroy tutorial 
        //set game speed to 1 
    }
    public void CloseTutorialMenu()
    {
        Time.timeScale = 1;
        Destroy(tutorialMenu);
        
       
    }
}
