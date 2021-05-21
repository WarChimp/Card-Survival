using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatLessPerk : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PerkData perkInfo;
    [SerializeField] private GameManager gameScript;
    private int foodSaved = 1;
    void Start()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        perkInfo.perkUnlocked = true;
        Debug.Log(perkInfo.perkName);

        Eatless();
    }

    // Update is called once per frame
    private void Eatless()
    {
        if(perkInfo.perkUnlocked)
        {
            gameScript.UpdateFoodUI(foodSaved);
            TextRecord.instance.PostMessage("You managed to not eat as much thanks to your perk: " + perkInfo.perkName);
        }
    }
}
