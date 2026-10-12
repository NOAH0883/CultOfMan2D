using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static IInteractable;

public class Sleep : MonoBehaviour, Interactable
{

    [SerializeField] InputActionProperty closeMenu;

    [SerializeField] VillageManager villageManager;
    [SerializeField] GameObject sleepani;


    [Header("Lighting")]
    [SerializeField] Light2D lighting;
    [SerializeField] GameObject campfireOn;
    

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
        DayStats();

        if (GameData.isDay)
        {
            lighting.intensity = 1;
            campfireOn.SetActive(false);
            
        }
        else
        {
            lighting.intensity = 0.01f;
            campfireOn.SetActive(true);
            


        }

    }

    public void Interact()
    {
       if(!GameData.isDay)
       {
            if(GameData.hasSacrificed)
            {
                GameData.hasSacrificed = false;
            }

            GameData.isDay = true;  
            GameData.food = 0;
            StartCoroutine(SleepAni());

            //players health gets reset to max

            villageManager.FeedVillagers();
            villageManager.BuildHouses();

            closeMenu.action.Enable();
            closeMenu.action.performed += OnCancel;
        }

    }




    void DayStats()
    {
        
        
        populationSumText.text = (GameData.population - villageManager.popIncrease).ToString() + " + " + villageManager.popIncrease; // + pop increase
        HousingSumText.text = (villageManager.housing - villageManager.housingIncrease).ToString() + " + " + villageManager.popIncrease; // + housing increase

        sickVillSumText.text = villageManager.villagersSick.ToString();
        deadVillSumText.text = villageManager.villagersDead.ToString();
        
        sacVillSumText.text = villageManager.villSac.ToString();
    }


    IEnumerator SleepAni()
    {
        sleepani.SetActive(true);
        yield return new WaitForSeconds(2f);
        sleepani.SetActive(false);
        dayStatsMenu.SetActive(true);
        Time.timeScale = 0f;
    }


    void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;

            if(villageManager.villSac > 1)
            {
                GameData.hasSacrificed = true;
                GameData.dailyGoal = (float)(GameData.dailyGoal * 1.5);


                //bool have sacrificed someone
                // if the bool is true 
                //food needed is 1.5x
                //villagers food needed is increased by 1.5
                //villages food 1.5x
            }


            //~~reset stats~~
            villageManager.popIncrease = 0;
            villageManager.housingIncrease = 0;
            villageManager.villagersSick = 0;
            villageManager.villagersDead = 0;
            villageManager.villSac = 0;
            // playerHealth reset
            

            dayStatsMenu.SetActive(false);
            Time.timeScale = 1f;

        }

    }



}
