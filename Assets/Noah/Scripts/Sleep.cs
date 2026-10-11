using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{
    [SerializeField] VillageManager villageManager;
    [SerializeField] GameObject sleepani;


    [Header("Lighting")]
    [SerializeField] Light2D lighting;
    [SerializeField] Color dayColour;
    [SerializeField] Color nightColour;


    [Header("Daily Summary")]
    [SerializeField] GameObject dayStatsMenu;
    [SerializeField] TextMeshProUGUI populationSumText;
    [SerializeField] TextMeshProUGUI HousingSumText;
    [SerializeField] TextMeshProUGUI sickVillSumText;
    [SerializeField] TextMeshProUGUI deadVillSumText;
    [SerializeField] TextMeshProUGUI sacVillSumText;


    void Start()
    {
        GameData.isDay = true;
        
        sleepani.SetActive(false);
        dayStatsMenu.SetActive(false);
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
       if(!GameData.isDay)
       {
            GameData.isDay = true;  
            GameData.food = 0;
            StartCoroutine(SleepAni());

            //players health gets reset to max

            villageManager.FeedVillagers();
            villageManager.BuildHouses();
       }

    }




    void DayStats()
    {
        
        dayStatsMenu.SetActive(true);
        populationSumText.text = GameData.population.ToString(); // + pop increase
        //HousingSumText.text = VillageManager.housing.ToString(); // + housing increase
        //HousingSumText.text = VillageManager.housing.ToString();
        
        //sickVillSumText.text = number of villages that got sick in the day
        //deadVillShText.text = number of villagers that die this day
        //sacVillSumText.text = number of villagers that were saced this day

    }


    IEnumerator SleepAni()
    {
        sleepani.SetActive(true);
        yield return new WaitForSeconds(2f);
        sleepani.SetActive(false);
    }


}
