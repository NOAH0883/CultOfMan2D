using UnityEngine;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{
    
    void Start()
    {
       
        Debug.Log("day");
    }
    


    public void Interact()
    {
       if(!GameData.isDay)
       {
            GameData.isDay = true;  
            GameData.hasFeedVillage = false;
            GameData.food = 0;

            
            //player the day cycle animation 
            // make new villages if needed
       }

    }


}
