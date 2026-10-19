
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

    InputSystem_Actions inputActions;


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



    
    bool menuOpen;

    GameObject lastSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        inputActions = new InputSystem_Actions();

        inputActions.Player.Disable();
        inputActions.UI.Disable();

        menuOpen = false;
       
        CampFireMenu.SetActive(false);
        villageManager = GetComponent<VillageManager>();
        
        sacButtonVisual.SetActive(false);
        sacButton.interactable = false;


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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            

            Time.timeScale = 1f;

            SceneManager.LoadScene("YouLose");

        }
        
        if (inputActions.UI.Cancel.WasPressedThisFrame())
        {
            CloseMenu();
        }

        ActiveButtons();
        Upgrades();
        CurrentSelection();


    }

    public void Interact()
    {
        if(!menuOpen)
        {
            menuOpen = true;
        }

        if (!GameData.isDay && GameData.food < GameData.dailyGoal)
        {
            canSac = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputActions.Player.Disable();
        inputActions.UI.Submit.Reset();


        CampFireMenu.SetActive(true);

        StartCoroutine(WaitFrame());
       

        Time.timeScale = 0f;
    }

    IEnumerator WaitFrame()
    {
        
        yield return new WaitForEndOfFrame();
        inputActions.UI.Enable();

        if(feedButton.interactable)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(feedButton.gameObject);
        }
        else if (sacButton.interactable)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(sacButton.gameObject);
        }

       

    }


    void CurrentSelection()
    {
        if (!CampFireMenu.activeSelf) return;

        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        // If nothing is selected, or the selected object is disabled / not interactable
        if (currentSelected == null || !currentSelected.activeInHierarchy ||
            (currentSelected.TryGetComponent<Button>(out Button btn) && !btn.interactable))
        {
            EventSystem.current.SetSelectedGameObject(null);

            if (feedButton != null && feedButton.interactable && feedButton.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(feedButton.gameObject);
            }
            else if (sacButton != null && sacButton.interactable && sacButton.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(sacButton.gameObject);
            }
            else if (housingButton != null && housingButton.interactable && housingButton.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(housingButton.gameObject);
            }
            else if (upgradeButton != null && upgradeButton.interactable && upgradeButton.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(upgradeButton.gameObject);
            }
        }
    }

    void CloseMenu()  
    {
        if (menuOpen)
        {
            inputActions.UI.Disable();
            inputActions.Player.Enable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            CampFireMenu.SetActive(false);
            Time.timeScale = 1f;

            menuOpen = false;
        }


    }



 

    void ActiveButtons()
    {
        //housing 

        if (GameData.food < 10 || villageManager.housing >= 20)
        {
            
            housingGreyOut.SetActive(true);
            housingButton.interactable = false;
        }
        else
        {
            housingButton.interactable = true;
            housingGreyOut.SetActive(false);
        }


        if (GameData.food >= 4 && !GameData.isDay)
        {
            feedText.text = "-4 FOOD";
            feedGreyOut.SetActive(false);
            feedButton.interactable = true;
        }
        else if (GameData.food > 0 && !GameData.isDay)
        {
            feedText.text = "Fill";
            feedGreyOut.SetActive(false);
            feedButton.interactable = true;
        }
        else
        {
            feedGreyOut.SetActive(true);
            feedButton.interactable = false;
        }

        if (!GameData.isDay && canSac)
        {
            sacButtonVisual.SetActive(true);
            sacButton.interactable = true;
        }
        else
        {
            sacButtonVisual.SetActive(false);
            sacButton.interactable = false;
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
            upgradeButton.interactable = false;
            return; 
        }

        
        upgradScriptableObj = upgradesList[0];

        if (GameData.food < upgradScriptableObj.cost)
        {
            
            upgradeGreyOut.SetActive(true);
            upgradeButton.interactable = false;
        }
        else
        {
            
            upgradeNameText.text = upgradScriptableObj.upgradeName;
            upgradeDescriptionText.text = upgradScriptableObj.destriptionText;
            upgradeCostText.text = "-" + upgradScriptableObj.cost.ToString() + " FOOD";
            upgradeImageSprite.sprite = upgradScriptableObj.icon;
            upgradeGreyOut.SetActive(false);
            upgradeButton.interactable = true;
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

}
