
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static IInteractable;


public class CampFire : MonoBehaviour, Interactable
{


    [SerializeField] GameObject CampFireMenu;
    [SerializeField] InputActionProperty closeMenu;
    [SerializeField] EventSystem eventSystem;
    GameObject lastSelected;

    VillageManager villageManager;


    [Header("Housing")]
    [SerializeField] GameObject housingGreyOut;
    [SerializeField] Button housingButton;

    [Header("Feed")]
    [SerializeField] TextMeshProUGUI feedText;
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
    UpgradScriptableObj upgradScriptableObj;
    [SerializeField] GameObject player;
    AdamPlayerTest playerScript;
    PlayerHitbox playerWeapon;

    [Header("Test")]
    [SerializeField] int foodTest;



    [SerializeField] GameObject campfireTutorial;
    bool firstTimeMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        firstTimeMenu = false;

        campfireTutorial.SetActive(false);
        CampFireMenu.SetActive(false);
        villageManager = GetComponent<VillageManager>();
        
        sacButtonVisual.SetActive(false);
        sacButton.enabled = false;


        playerScript = player.GetComponent<AdamPlayerTest>();
        playerWeapon = player.GetComponentInChildren<PlayerHitbox>();


    }

    void Update()
    {
        if (GameData.population <= 0)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Destroy(player);
            GameObject soundManager = GameObject.FindWithTag("SoundManager");
            Destroy(soundManager);

            closeMenu.action.Disable();
            closeMenu.action.performed -= OnCancel;

            Time.timeScale = 1f;

            SceneManager.LoadScene("YouLose");

        }


        ActiveButtons();
        Upgrades();
        keepSelected();
    }

    public void Interact()
    {

        
        closeMenu.action.performed += OnCancel;


        if(!firstTimeMenu && !GameData.isDay)
        {
            Debug.Log("===================================Load tutorial for campfire");
            campfireTutorial.SetActive(true);
        }
        else
        {
            CampFireMenu.SetActive(true);
        }

        StartCoroutine(WaitFrame());

        if (!GameData.isDay && GameData.food < GameData.dailyGoal)
        {
            canSac = true;
        }

        if (!GameData.isDay && canSac)
        {
            sacButtonVisual.SetActive(true);
            sacButton.enabled = true;
        }
        

        Time.timeScale = 0f;
    }

    IEnumerator WaitFrame()
    {
        yield return 0;


        if (feedButton.IsActive())
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(feedButton.gameObject);
        }
        else if (sacButton.IsActive())
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(sacButton.gameObject);
        }
        
    }

    void keepSelected()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            // Only update if it's a completely new object
            if (EventSystem.current.currentSelectedGameObject != lastSelected)
            {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
        }
        // 2. If selection becomes null, safely restore the last one
        else if (lastSelected != null)
        {
            StartCoroutine(RestoreSelectionRoutine());
        }
    }

    private IEnumerator RestoreSelectionRoutine()
    {
        // Wait until the very end of the current frame 
        // This stops Unity from glitching or getting stuck in a loop
        yield return new WaitForEndOfFrame();

        // Double check it's still null before restoring
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }



    void OnCancel (InputAction.CallbackContext context)  
    {
       if(context.performed)
       {
            
            if(!firstTimeMenu)
            {
                campfireTutorial.SetActive(false);
                closeMenu.action.performed -= OnCancel;
                firstTimeMenu = true;
                Interact();
            }
            else
            {
                closeMenu.action.performed -= OnCancel;


                CampFireMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            
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


        if (GameData.food >= 4 && !GameData.isDay)
        {
            feedText.text = "-4 FOOD";
            feedGreyOut.SetActive(false);
            feedButton.enabled = true;
        }
        else if (GameData.food > 0 && !GameData.isDay)
        {
            feedText.text = "Fill";
            feedGreyOut.SetActive(false);
            feedButton.enabled = true;
        }
        else
        {
            feedGreyOut.SetActive(true);
            feedButton.enabled = false;
        }

        //if (GameData.food > 4)
        //{
        //    feedText.text = "-4 FOOD";
        //    feedGreyOut.SetActive(false);
        //    feedButton.enabled = true;
        //}
        //else
        //{

        //    if(!GameData.isDay)
        //    {
        //        canSac = true;
        //    }

        //    if (GameData.food > 0)
        //    {
        //        feedText.text = "FILL";
        //        feedGreyOut.SetActive(false);
        //        feedButton.enabled = true;
        //    }
        //    else
        //    {
        //        feedButton.enabled = false;
        //        feedGreyOut.SetActive(true);
        //    }
        //}


        // if player has more than 4 food 
        // turn on food button 
        //else if 
        //player has less than 4 food 
        // if food is greater than 0
        // button turns to fill 
        //else if
        //player <= 0 
        // button get turned off 



        // Population
        popUIText.text = GameData.population.ToString() + " / " + villageManager.housing.ToString();


        //Food
        foodUIText.text = GameData.food.ToString();

        //villages feed
        villagersFeedUIText.text = villageManager.villagersFeed.ToString() + " / " + GameData.population;
    }


    void Upgrades()
    {

        if (upgradesList == null || upgradesList.Count == 0)
        {
            
            upgradScriptableObj = null;
            upgradeNameText.text = null;
            upgradeDescriptionText.text = null;
            upgradeCostText.text = null;
            upgradeImageSprite.sprite = null;
            upgradeGreyOut.SetActive(true);
            upgradeButton.enabled = false;
            return; 
        }

        
        upgradScriptableObj = upgradesList[0];

        if (GameData.food < upgradScriptableObj.cost)
        {
            
            upgradeGreyOut.SetActive(true);
            upgradeButton.enabled = false;
        }
        else
        {
            
            upgradeNameText.text = upgradScriptableObj.upgradeName;
            upgradeDescriptionText.text = upgradScriptableObj.destriptionText;
            upgradeCostText.text = upgradScriptableObj.cost.ToString();
            upgradeImageSprite.sprite = upgradScriptableObj.icon;
            upgradeGreyOut.SetActive(false);
            upgradeButton.enabled = true;
        }

    }

    public void UpgradeTest()
    {
        SoundManager.PlaySound(SoundType.upgrade, 0.7f);

        GameData.food -= upgradScriptableObj.cost;

        playerScript.playerHitboxScript.damage += upgradScriptableObj.damageBonus;
        playerScript.playerMaxHealth += upgradScriptableObj.healthBonus;

        upgradesList.RemoveAt(0);
    }



    private void OnDisable()
    {
        closeMenu.action.Disable();
        closeMenu.action.performed -= OnCancel;
    }

}
