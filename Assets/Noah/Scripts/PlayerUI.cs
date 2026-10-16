using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI foodNeededText;
    AdamPlayerTest playerController;

    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [SerializeField] Image[] hearts;

    [SerializeField] GameObject KockedOutMenu;
    [SerializeField] GameObject bloodVisual;

    SceneLoader sceneLoader;

    private void Start()
    {
        playerController = GetComponentInParent<AdamPlayerTest>();
        
        KockedOutMenu.SetActive(false);
        bloodVisual.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.hasSacrificed)
        {
            bloodVisual.SetActive(true);
            if(GameData.isDay)
            {
                GameData.dailyGoal = (int)(GameData.population * 2) * 1.5f;
            }
            
        }
        else
        {
            bloodVisual.SetActive(false);
            GameData.dailyGoal = GameData.population * 2;
        }
        foodNeededText.text = "Food " + GameData.food.ToString() + " / " + GameData.dailyGoal.ToString();


        for (int i = 0; i < hearts.Length; i++)
        {
            if(i <= playerController.playerHealth)
            {
                
                hearts[i].sprite = fullHeart;

            }
            else if(i >= playerController.playerHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            
            if (i < playerController.playerMaxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }  
        }
    }

    public void playerKnockedOut()
    {

        Time.timeScale = 0;
        StartCoroutine(KnockedOutVisual());
        
    }

    IEnumerator KnockedOutVisual()
    {
        Time.timeScale = 0;
        GameData.food /= 2;
        if(GameData.food < 0)
        {
            GameData.food = 0;
        }
        KockedOutMenu.SetActive(true); 
        yield return new WaitForSecondsRealtime(5f);
        KockedOutMenu.SetActive(false);
        Time.timeScale = 1;

        sceneLoader = FindAnyObjectByType<SceneLoader>();
        sceneLoader.LoadVillage();
        

    }




}
