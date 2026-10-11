using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObjects/UpgradScriptableObj")]
public class UpgradScriptableObj : ScriptableObject
{

    public string upgradeName;
    public string destriptionText;
    public Sprite icon;

    public int cost;

    //health
    public float healthBonus;

    //damage
    public float damageBonus;

    //Weapon
    public GameObject newWeapon;

}
