using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card Survival/Card", order = 0)]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public int actionPointCost;

    public Sprite cardImage;


    public void DisplayInformation()
    {
        Debug.Log(cardName);
    }
}
