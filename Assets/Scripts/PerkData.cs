using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerkData", menuName = "Card Survival/Perks", order = 0)]
public class PerkData : ScriptableObject
{
    public string perkName;
    public string perkDescription;

    public int dayUnlocked;
    public int perkWoodCost;
    public int perkFoodCost;
    public bool perkUnlocked;
}
