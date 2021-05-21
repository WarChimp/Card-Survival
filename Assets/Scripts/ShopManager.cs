using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private int maxItems = 6;
    private int currentItems;
    private bool _hasPurchased = false;
    public bool _changeShop = true;

    [SerializeField] private Text[] _CardName;
    [SerializeField] private GameObject[] _cardTemplate;

    [SerializeField] private GameObject shopScreen;

    [SerializeField] private Player playerScript;
    [SerializeField] private GameManager gameScript;
    [SerializeField] private Transform[] shopCardSpawnPoints;
    [SerializeField] private Transform _canvasParent;

    [SerializeField] private Sprite[] _cardSprites;
    [SerializeField] private GameObject _ImageToChange;

    private GameObject[] cardChosen = new GameObject[3];
    private GameObject[] cardMemory;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentItems = maxItems;
    }
    public void BuildShopFront()
    {
        cardMemory = GameObject.FindGameObjectsWithTag("Card");
        foreach (var card in cardMemory)
        {
            card.SetActive(false);
        }
        int i = 0;
        if(_changeShop)
        {
            foreach (var name in _CardName)
            {
                var _image = _cardTemplate[i].transform.Find("Image").GetComponent<Image>();
                int randNo = Random.Range(0, gameScript.cards.Length);
                cardChosen[i] = gameScript.cards[randNo];
                name.text = cardChosen[i].GetComponent<CardData>().cardName;
                var chosenCardName = name.text.ToString();
                Debug.Log("chosenCardName is: " + chosenCardName);

                switch(chosenCardName)
                {
                    case "Draw Card":
                        _image.GetComponent<Image>().sprite = _cardSprites[0];
                        break;
                    case "Gather":
                        _image.GetComponent<Image>().sprite = _cardSprites[1];
                        break;
                    case "Hunting":
                        _image.GetComponent<Image>().sprite = _cardSprites[2];
                        break;
                    case "Refreshing Activity":
                        _image.GetComponent<Image>().sprite = _cardSprites[3];
                        break;
                    case "Salt Card":
                        _image.GetComponent<Image>().sprite = _cardSprites[4];
                        break;
                    case "Stoke Fire":
                        _image.GetComponent<Image>().sprite = _cardSprites[5];
                        break;
                    case "Trap Card":
                        _image.GetComponent<Image>().sprite = _cardSprites[6];
                        break;
                }
                //_ImageToChange.GetComponent<Image>().sprite = _cardSprites[i];
                Debug.Log(cardChosen);
                i++;
            }

            _changeShop = false;
        }

    }

    public bool HasBoughtToday()
    {
        if (!_hasPurchased)
            return false;
        else
            return true;
    }

    public void EndOfDay()
    {
        _hasPurchased = false;
        _changeShop = true;
    }

    public void CloseShop()
    {
        shopScreen.SetActive(false);
        foreach (var card in cardMemory)
        {
            card.SetActive(true);
        }
    }

    public void PurchaseButton(int i)
    {
        //var cardClicked = GetComponent<CardData>().cardName;
        if(!HasBoughtToday())
        {
            _hasPurchased = true;
            Debug.Log("<color=red>The card that was clicked was</color>: " + cardChosen);
            //cardChosen.SetActive(false);
            Player.instance.playerDeck.Insert(0, cardChosen[i]);
            SoundManager.instance.PlaySound(4);
        }
       
    }


}
