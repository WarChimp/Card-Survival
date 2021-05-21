using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class TextResourcePopup : MonoBehaviour
{

    [SerializeField] private Text _popupText;
    [SerializeField] private Transform[] _popupTextSpawn;

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = _popupText.GetComponent<Animator>();
        Debug.Log(_popupTextSpawn[0].position);
    }

    
    public void GetTextPopupLocation(string resource, int amountOfResourceGathered, bool choice)
    {
        switch(resource)
        {
            case "food":
                PopupText(choice, amountOfResourceGathered, _popupTextSpawn[0].transform);                
                break;
            case "wood":
                PopupText(choice, amountOfResourceGathered, _popupTextSpawn[1].transform);
                break;
            case "temp":
                PopupText(choice, amountOfResourceGathered, _popupTextSpawn[2].transform);
                break;
        }
    }

    private void PopupText(bool prefix, int amount, Transform location)
    {
        Instantiate(_popupText, location.position, location.rotation);

        switch(prefix)
        {
            case true:
                _popupText.text = "+" + amount.ToString();
                StartCoroutine(PlayAnimation());
                break;
            case false:
                _popupText.text = "-" + amount.ToString();
                StartCoroutine(PlayAnimation());
                break;
        }
        //_popupText.text = "+" + amount.ToString();
        //StartCoroutine(PlayAnimation());

    }

    IEnumerator PlayAnimation()
    {
        _anim.SetBool("playAnimation", true);
        yield return new WaitForSeconds(0.8f);
        _anim.SetBool("playAnimation", false);
    }

}
