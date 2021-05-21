using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : MonoBehaviour
{

    [SerializeField] private Sprite[] _cardImages;
    [SerializeField] private GameObject _cardDisplay;

    [SerializeField] private GameObject _playerDeckInterface;

    [SerializeField] private Text _howManyCards;
    [SerializeField] private GameObject _spriteToChange;



    // Start is called before the first frame update

    public void OpenPlayerDeck()
    {
        _playerDeckInterface.SetActive(true);
        DisplayPlayerDeck(1);
    }

    public void ClosePlayerDeck()
    {
        _playerDeckInterface.SetActive(false);
    }

    private void DisplayPlayerDeck(int number)
    {
        switch(number)
        {
            case 1:
                _spriteToChange.GetComponent<Image>().sprite = _cardImages[1];
                Debug.Log("test");
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
