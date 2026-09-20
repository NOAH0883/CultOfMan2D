using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI foodNeededText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameData.dailyGoal = GameData.population * 2;
        foodNeededText.text = "Food needed " + GameData.food.ToString() + " / " + GameData.dailyGoal.ToString();
    }
}
