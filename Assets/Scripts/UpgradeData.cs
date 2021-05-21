using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Card Survival/Upgrades", order = 1)]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int foodCost;
    public int woodCost;
    public Sprite icon;
}
