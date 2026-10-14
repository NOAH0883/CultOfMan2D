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

    private void Start()
    {
        playerController = GetComponentInParent<AdamPlayerTest>();  
    }

    // Update is called once per frame
    void Update()
    {

        GameData.dailyGoal = GameData.population * 2;
        foodNeededText.text = "Food " + GameData.food.ToString() + " / " + GameData.dailyGoal.ToString();

        //I = 0  - - - player health = 5
        //if I < player health
        //make full sprite
        //if I > player health 
        //make empty sprite


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
}
