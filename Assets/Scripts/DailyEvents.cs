using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DailyEvents : MonoBehaviour
{

    public GameObject[] dailyEventCards;
    private GameManager gameScript;
    private GameObject cardEvent;

    private CardManagement saltedFoodChecker = new CardManagement();

    private bool _huntingDilemma = false;
    private bool _heatWaveOn = false;

    private Vector3 cardSpawn = new Vector3(-5f, -7f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        gameScript = gameObject.GetComponent<GameManager>();
    }

    public void cardPick(int number)
    {
        switch(number)
        {
            case 0:
                HeatWave();
                break;
            case 1:
                HuntingDilemma();
                break;
            case 2:
                SnowStorm();
                break;
            default:
                Debug.Log("There was a problem");
                break;
        }
    }
    public void SnowStorm()
    {
        Debug.Log("SNOWSTORM");
        cardEvent = Instantiate(dailyEventCards[2], cardSpawn, transform.rotation);
        CardData cardInfo = cardEvent.gameObject.GetComponent<CardData>();
        TextRecord.instance.PostMessage(cardInfo.cardDescription);


        //At some point we need to check what cards the player has
        //This is where we would run that check
        //If that card doesn't exist then activate the card effect

        gameScript.tempAmount -= cardInfo.tempAmount;
        gameScript.tempCounter.text = gameScript.tempAmount.ToString();
    }

    public void HeatWave()
    {
        _heatWaveOn = true;
        TextRecord.instance.PostMessage("You are experiencing a heat wave");
        GameEffects.DisplayIcon_Static(1);  
    }

    public void HeatWaveActivate()
    {
        cardEvent = Instantiate(dailyEventCards[0], cardSpawn, transform.rotation);
        CardData cardInfo = cardEvent.gameObject.GetComponent<CardData>();

        if (saltedFoodChecker._hasSaltedFood == false)
        {
            TextRecord.instance.PostMessage(cardInfo.cardDescription);
            gameScript.tempAmount -= cardInfo.tempAmount;
            gameScript.tempCounter.text = gameScript.tempAmount.ToString();

            gameScript.foodAmount -= cardInfo.meatAmount;
            gameScript.foodCounter.text = gameScript.foodAmount.ToString();

            if (gameScript.foodAmount < 0)
            {
                gameScript.foodAmount = 0;
                gameScript.UpdateFoodUI(0);
            }
        }
        else
        {
            TextRecord.instance.PostMessage("You are experiencing a heat wave. Luckily your food is safe due to it being preserved");
        }
    }

    public bool HeatWaveCheck()
    {
        if (_heatWaveOn)
            return true;
        else
            return false;
    }

    public void HeatWaveOff()
    {
        _heatWaveOn = false;
    }

    public void HuntingDilemma()
    {
        GameEffects.DisplayIcon_Static(3);

        TextRecord.instance.PostMessage("Your chances of finding something on a hunt today is low");
        _huntingDilemma = true;

        Instantiate(dailyEventCards[1], cardSpawn, transform.rotation);
    }

}
