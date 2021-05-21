using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    public static Player instance;

    public int actionPoints = 3;
    public int maxActionPoints = 3;
    public int drawAmount = 3;
    public int maximumDeckSize = 15;

    public int handSize = 3;
    public int defaultHandSize = 3;

    //Upgrades
    public bool fishingPortActive = false;

    public List<GameObject> playerHand;
    public List<GameObject> playerDeck;
    public List<GameObject> playerGraveyard;

    public GameManager cardSpawner;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cardSpawner = GameObject.Find("GameManager").GetComponent<GameManager>();
        CreateDefaultDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CardCount(string cardName)
    {
        int cardCount = 0;
        foreach (var card in playerDeck)
        {
            var cardToCheck = card.GetComponent<CardData>().cardName;
            if(cardToCheck == cardName)
            {
                cardCount++;
            }
        }

        return cardCount;
    }

    void CreateDefaultDeck()
    {
        for (int i = 0; i < maximumDeckSize; i++)
        {
            int randNo = Random.Range(0, cardSpawner.cards.Length);
            playerDeck.Add(cardSpawner.cards[randNo]);
        }
    }

    public void CardDraw(Transform spawnPoint, Vector3 offset, float rotationOffset, int drawAmount)
    {
        //spawnPoint.rotation * Quaternion.Euler(0f, 0f, rotationOffset)
        float angle = 2 * Mathf.PI / drawAmount;
        playerHand.Add(playerDeck[0]);
        Instantiate(playerDeck[0], spawnPoint.position + offset, Quaternion.identity);
        //Instantiate(playerDeck[0], (spawnPoint.x, spawnPoint.y, spawnPoint.z), spawnPoint.rotation * Quaternion.Euler(0f, 0f, rotationOffset));

        playerGraveyard.Add(playerDeck[0]);
        playerDeck.Remove(playerDeck[0]);
    }

    public void ClearHand()
    {
        playerHand.Clear();
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in cards)
        {
            GameObject.Destroy(card);
        }
    }

    public bool HasEnoughActionPoints(int actionPointsToSpend)
    {
        if (actionPointsToSpend <= actionPoints)
            return true;
        else
            return false;
    }

    public void UseActionPoints(int actionPointsToSpend)
    {
        actionPoints -= actionPointsToSpend;
        cardSpawner.UpdateActionPointUI(actionPoints);
    }


    public bool CantDrawMore()
    {
        if (drawAmount > playerDeck.Count)
            return true;
        else
            return false;
    }
    public void ResetDeck()
    {
        foreach (var card in playerGraveyard)
        {
            playerDeck.Add(card);
        }

        playerGraveyard.Clear();
    }


    private bool TestingEncapsulation()
    {
        if(playerDeck.Count >= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void TestingCoolMethods()
    {
        if (TestingEncapsulation() == true)
            Debug.Log("Encapsulation successful");
        else
            Debug.Log("Still successful but just false");
    }

    public void BuildFishingPort()
    {
        fishingPortActive = true;
    }

    public void ActionPointUpgrade()
    {
        maxActionPoints += 1;
    }

}
