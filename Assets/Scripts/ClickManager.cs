using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private CardData cardScripts;
    private CardManagement cardFunction;
    [SerializeField] private GameObject cardManager;
    private Collider2D col;

    private GameEffects iconEffects; 
    // Start is called before the first frame update
    void Start()
    {
        iconEffects = new GameEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name + " was clicked!");

                CardClickedFunction(hit);
            }
        }

        
    }

    private void CardClickedFunction(RaycastHit2D hit)
    {
        cardScripts = hit.collider.GetComponent<CardData>();
        cardFunction = cardManager.GetComponent<CardManagement>();

        Debug.Log(cardScripts.cardName);
        Debug.Log(cardScripts.actionPointCost);


        if (cardScripts.cardName == "Hunting")
        {
            cardFunction.HuntingCard(cardScripts.defaultHuntChance, cardScripts.meatAmount, cardScripts.tempAmount, cardScripts.actionPointCost, hit);
        }
        else if (cardScripts.cardName == "Stoke Fire")
        {
            cardFunction.StokeFire(cardScripts.woodAmount, cardScripts.tempAmount, cardScripts.actionPointCost, hit, cardScripts.cardDescription);

        }
        else if (cardScripts.cardName == "Gather")
        {
            cardFunction.GatherCard(cardScripts.woodAmount, cardScripts.tempAmount, cardScripts.actionPointCost, hit, cardScripts.cardDescription);

        }
        else if (cardScripts.cardName == "Refreshing Activity")
        {
            cardFunction.RefreshDayActionPoints(cardScripts.actionPointCost, hit);
            TextRecord.instance.PostMessage(cardScripts.cardDescription);

        }
        else if (cardScripts.cardName == "Draw Card")
        {
            cardFunction.DrawExtraCard(cardScripts.actionPointCost, hit, cardScripts.cardDescription);

        }
        else if (cardScripts.cardName == "Salt Card")
        {
            cardFunction.ToSaltFood(cardScripts.actionPointCost, cardScripts.tempAmount, hit, cardScripts.cardDescription);
        }
        else if (cardScripts.cardName == "Trap Card")
        {
            cardFunction.Trapping(cardScripts.actionPointCost, hit);
        }


        //Destroy(hit.collider.gameObject);
    }

    private void ReturnCardLocation()
    {
        
    }
}
