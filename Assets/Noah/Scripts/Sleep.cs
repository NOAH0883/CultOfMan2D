using UnityEngine;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{
    VillageManager villageManager;
    void Start()
    {
        villageManager = Object.FindAnyObjectByType<VillageManager>();
        Debug.Log("day");
    }
    


    public void Interact()
    {
       if(!GameData.isDay && GameData.hasFeedVillage)
       {
            GameData.isDay = true;  
            GameData.hasFeedVillage = false;
            GameData.food = 0;

            villageManager.OverNight();

            //player the day cycle animation 
            // make new villages if needed
       }

    }


}
