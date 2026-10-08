using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI foodNeededText;

    // Update is called once per frame
    void Update()
    {

        GameData.dailyGoal = GameData.population * 2;
        foodNeededText.text = "Food " + GameData.food.ToString() + " / " + GameData.dailyGoal.ToString();
    }
}
