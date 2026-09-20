using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{
    VillageManager villageManager;
    [SerializeField] GameObject sleepani;
    void Start()
    {
        villageManager = Object.FindAnyObjectByType<VillageManager>();
        Debug.Log("day");
        sleepani.SetActive(false);
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
