
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.UI;
using static IInteractable;


public class CampFire : MonoBehaviour, Interactable
{

    [SerializeField] GameObject CampFireMenu;
    [SerializeField] InputActionProperty closeMenu;
   

    VillageManager villageManager;


    [Header("Housing")]
    [SerializeField] GameObject housingGreyOut;
    [SerializeField] Button housingButton;

    [Header("Feed")]
    [SerializeField] GameObject feedGreyOut;
    [SerializeField] Button feedButton;

    [Header("Upgrades")]
    [SerializeField] GameObject upgradeGreyOut;
    [SerializeField] Button upgradeButton;

    [Header("Sacrifice")]
    [SerializeField] GameObject sacButtonVisual;
    [SerializeField] Button sacButton;
    bool canSac;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI popUIText;
    [SerializeField] TextMeshProUGUI foodUIText;
    [SerializeField] TextMeshProUGUI villagersFeedUIText;

    [Header("Upgrades")]
    [SerializeField] List<UpgradScriptableObj> upgradesList;
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] TextMeshProUGUI upgradeCostText;
    [SerializeField] Image upgradeImageSprite;


    [Header("Test")]
    [SerializeField] int foodTest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CampFireMenu.SetActive(false);
        villageManager = GetComponent<VillageManager>();
        
    }

    void Update()
    {
        ActiveButtons();
        Upgrades();
    }

    public void Interact()
    {
        GameData.hasFeedVillage = true;
        closeMenu.action.Enable();
        closeMenu.action.performed += OnCancel;

        CampFireMenu.SetActive(true);
        Time.timeScale = 0f;
    }

  
    void OnCancel (InputAction.CallbackContext context)  
    {
       if(context.performed)
       {
            
            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;


            CampFireMenu.SetActive(false);
            Time.timeScale = 1f;
            
        }
            
    }



    public void Sacrifice()
    {
        if(GameData.population > 0 )
            villageManager.Sacrifice();
        
    }

    public void UpgradeWeapon()
    {
        if(GameData.food>3 && !GameData.hasWeapon)
        {
            GameData.hasWeapon = true;
            Debug.Log("Give playerWeapon");
            GameData.food -= 3;
        }
        else
        {
            Debug.Log("Not enough food");
        }
    } 



    void ActiveButtons()
    {
        //housing 

        if (GameData.food < 10 || villageManager.housing >= 20)
        {
            
            housingGreyOut.SetActive(true);
            housingButton.enabled = false;
        }
        else
        {
            housingButton.enabled = true;
            housingGreyOut.SetActive(false);
        }
   
        //sacrifice && feed
        if (GameData.food < 5)
        {
            feedButton.enabled = false;
            feedGreyOut.SetActive(true);
            canSac = true;
        }
        else
        {
            feedGreyOut.SetActive(false);
            feedButton.enabled = true; 
        }


        if (!GameData.isDay && canSac)
        {
            sacButtonVisual.SetActive(true);
            sacButton.enabled = true;
        }




        // Population
        popUIText.text = GameData.population.ToString() + " / " + villageManager.housing.ToString();


        //Food
        foodUIText.text = GameData.food.ToString();

        //villages feed
        villagersFeedUIText.text = villageManager.villagersFeed.ToString() + " / " + GameData.population;
    }


    void Upgrades()
    {

        UpgradScriptableObj upgradScriptableObj = upgradesList[0];
        upgradeNameText.text = upgradScriptableObj.upgradeName;
        upgradeDescriptionText.text = upgradScriptableObj.destriptionText;
        upgradeCostText.text = upgradScriptableObj.cost.ToString();
        upgradeImageSprite.sprite = upgradScriptableObj.icon;



        //if food is > cost || list is equal to null
        //disable upgrade
        //else
        //enable upgrade


        //when button is pressed
        //give player the upgrades

        // remove the weapon from the list
    }

    public void UpgradeTest()
    {
        upgradesList.RemoveAt(0);
    }


}
