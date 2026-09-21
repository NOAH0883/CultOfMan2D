using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{
    VillageManager villageManager;
    [SerializeField] GameObject sleepani;



    [SerializeField] Light2D lighting;
    [SerializeField] Color dayColour;
    [SerializeField] Color nightColour;

    void Start()
    {
        villageManager = Object.FindAnyObjectByType<VillageManager>();
        Debug.Log("day");
        sleepani.SetActive(false);
    }
    
    void Update()
    {
        if (GameData.isDay)
        {
            lighting.color = dayColour;
        }
        else
        {
            lighting.color = nightColour;
        }

    }

    public void Interact()
    {
       if(!GameData.isDay && GameData.hasFeedVillage)
       {
            GameData.isDay = true;  
            GameData.hasFeedVillage = false;
            GameData.food = 0;

            villageManager.OverNight();
            StartCoroutine(SleepAni());
            //player the day cycle animation 
            // make new villages if needed
        }

    }


    IEnumerator SleepAni()
    {
        sleepani.SetActive(true);
        yield return new WaitForSeconds(2f);
        sleepani.SetActive(false);
    }


}
