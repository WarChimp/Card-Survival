using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManagement : MonoBehaviour
{
    [SerializeField] private GameManager gameScript;
    [SerializeField] private Player playerScript;
    private TextResourcePopup _resourceTextPopup;

    /*private int gatherAmount = 2;
    private int huntedAmount = 2;
    private float tempIncrease = 2f;*/

    public bool _hasSaltedFood = false;
    public bool _hasSetTrap = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameScript.totalActionPoints);
        Debug.Log(playerScript.actionPoints);
        _resourceTextPopup = GameObject.Find("GameManager").GetComponent<TextResourcePopup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GatherCard(int gatherAmount, float tempAmount, int actionPointCost, RaycastHit2D hit, string cardDescription)
    {

        if (playerScript.HasEnoughActionPoints(actionPointCost))
        {
            gameScript.UpdateWoodUI(gatherAmount);
            gameScript.UpdateTempUI(-tempAmount * 2);
            playerScript.UseActionPoints(actionPointCost);
            //gameScript.UpdateActionPointUI(actionPointCost);
            Destroy(hit.collider.gameObject);
            HoverTextBox.HideTooltip_Static();
            TextRecord.instance.PostMessage(cardDescription);

            ResourceGained.instance.GainResources(gatherAmount, 2);
            ResourceGained.instance.LoseResource(tempAmount, 1);


        }
        else
        {
            TextRecord.instance.PostMessage("Sorry you do not have enough points to perform that action");

        }

    }

    public void HuntingCard(int huntingChance, int huntedAmount, float tempAmount, int actionPointCost, RaycastHit2D hit)
    {
        int randFood = Random.Range(1, huntedAmount);
        if (playerScript.HasEnoughActionPoints(actionPointCost))
        {
            int randNo = Random.Range(0, 100);
            if(randNo <= huntingChance)
            {
                gameScript.UpdateFoodUI(randFood);
                gameScript.UpdateTempUI(-tempAmount);
                playerScript.UseActionPoints(actionPointCost);
                TextRecord.instance.PostMessage("You managed to find a bounty today of: " + randFood.ToString());

                ResourceGained.instance.GainResources(randFood, 0);
                ResourceGained.instance.LoseResource(tempAmount, 1);
            }
            else
            {
                TextRecord.instance.PostMessage("You were unable to find anything today");
                playerScript.UseActionPoints(actionPointCost);
                //gameScript.UpdateActionPointUI(actionPointCost);
            }
            Destroy(hit.collider.gameObject);
            HoverTextBox.HideTooltip_Static();


        }
        else
        {
            TextRecord.instance.PostMessage("Sorry you do not have enough points to perform that action");
        }

    }

    public void StokeFire(int woodAmount, float tempAmount, int actionPointCost, RaycastHit2D hit, string cardDescription)
    {
        if(playerScript.HasEnoughActionPoints(actionPointCost))
        {
            if(gameScript.woodAmount >= 1)
            {
                gameScript.UpdateWoodUI(-woodAmount);
                gameScript.UpdateTempUI(tempAmount);
                playerScript.UseActionPoints(actionPointCost);
                TextRecord.instance.PostMessage(cardDescription);
                ResourceGained.instance.GainResources(tempAmount, 1);
                ResourceGained.instance.LoseResource(woodAmount, 2);

            }
            else
            {
                TextRecord.instance.PostMessage("You have no wood remaining!");
                playerScript.UseActionPoints(actionPointCost);
            }
            Destroy(hit.collider.gameObject);
            HoverTextBox.HideTooltip_Static();
        }
        else
        {
            Debug.Log("Sorry you do not have enough points to perform that actions");
        }

    }

    public void ToSaltFood(int actionPointCost, float tempToUpdate, RaycastHit2D hit, string cardDescription)
    {
        if (playerScript.HasEnoughActionPoints(actionPointCost))
        {
            _hasSaltedFood = true;
            playerScript.UseActionPoints(actionPointCost);
            gameScript.UpdateTempUI(-tempToUpdate);
            Destroy(hit.collider.gameObject);
            HoverTextBox.HideTooltip_Static();
            TextRecord.instance.PostMessage(cardDescription);
            GameEffects.DisplayIcon_Static(2);

            ResourceGained.instance.LoseResource(tempToUpdate, 1);
        }
        else
        {
            TextRecord.instance.PostMessage("You do not have enough points to use this card");
        }

    }

    public void SetSaltedFood()
    {
        if (_hasSaltedFood == true)
            _hasSaltedFood = false;
    }

    public void DrawExtraCard(int actionPointCost, RaycastHit2D hit, string cardDescription)
    {
        if(playerScript.HasEnoughActionPoints(actionPointCost))
        {
            Player.instance.drawAmount++;
            Player.instance.UseActionPoints(actionPointCost);
            Destroy(hit.collider.gameObject);
            HoverTextBox.HideTooltip_Static();
            GameEffects.DisplayIcon_Static(0);

            TextRecord.instance.PostMessage(cardDescription);
        }
        else
        {
            TextRecord.instance.PostMessage("You do not have enough action points to use this");
        }
        


    }

    public void RefreshDayActionPoints(int actionPointsToRefresh, RaycastHit2D hit)
    {
        playerScript.UseActionPoints(-actionPointsToRefresh);
        Destroy(hit.collider.gameObject);
        HoverTextBox.HideTooltip_Static();


        //gameScript.RefreshActionPoints(playerScript.actionPoints, playerScript.maxActionPoints, actionPointsToRefresh);
    }

    public void Trapping(int actionPointsToRefresh, RaycastHit2D hit)
    {
        if(_hasSetTrap == false)
        {
            if (Player.instance.HasEnoughActionPoints(actionPointsToRefresh))
            {
                _hasSetTrap = true;
                GameEffects.DisplayIcon_Static(4);
                TextRecord.instance.PostMessage("You set up a trap, to hopefully catch something");
                Destroy(hit.collider.gameObject);
                HoverTextBox.HideTooltip_Static();
            }
        }
        else
        {
            TextRecord.instance.PostMessage("You already have a trap setup!");
        }
        
    }

    public bool IsTrapActive()
    {
        if (_hasSetTrap)
            return true;
        else
            return false;
    }

    public void CheckTraps()
    {
        if(_hasSetTrap == true)
        {
            var randNo = Random.Range(0, 100);
            var meatAmount = Random.Range(1, 3);

            if(randNo <= 25)
            {
                TextRecord.instance.PostMessage("You check your traps, but unfortunately find nothing\n You reset your trap");

            } else if(randNo > 25 && randNo <= 75)
            {
                TextRecord.instance.PostMessage("You check your traps and manage to find " + meatAmount + " morsel of food");
                _hasSetTrap = false;
                ResourceGained.instance.GainResources(meatAmount, 0);
                GameEffects.ClearSpecificIcon_Static(4);

            } else if (randNo > 75 && randNo <= 100)
            {
                TextRecord.instance.PostMessage("Unfortunately it looks like your traps have been broken");
                _hasSetTrap = false;
                GameEffects.ClearSpecificIcon_Static(4);
            }
        }
    }


}
