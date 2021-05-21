using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{

    private SpriteRenderer sr;
    private int originalOrder;
    private int sortingOrder;


    void Start()
    {
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        originalOrder = sr.sortingOrder;

    }

    
    void OnMouseEnter()
    {
        var cardDescription = gameObject.GetComponent<CardData>().cardDescription;
        transform.localScale = new Vector3(2f, 2f, 0);
        sr.sortingOrder = 100;

        HoverTextBox.ShowTooltip_Static(cardDescription);

    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 0);
        sr.sortingOrder = originalOrder;

        HoverTextBox.HideTooltip_Static();
    }
}
