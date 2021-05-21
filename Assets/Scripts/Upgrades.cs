using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{

    [SerializeField] private GameObject _upgradeUI;
    [SerializeField] private Sprite[] _upgradeIcons;
    [SerializeField] private Text _woodUI;
    [SerializeField] private Text _foodUI;
    [SerializeField] private UpgradeData[] upgradeInformation;
    [SerializeField] private GameObject fishShack;

    [SerializeField] private Transform parent;

    private int foodRequired;
    private int woodRequired;

    private GameManager gameScript;

    public bool fishingUpgrade = false;
    public bool actionPointUpgrade = false;
    public bool scarfUpgrade = false;

    public float scarfWarmth = -5f;

    private void Awake()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    // Start is called before the first frame update
    private void Start()
    {
        FishUpgradeUICreate();
        ActionPointUICreate();
        ScarfUICreate();
    }

    private void CreateFishingPort()
    {        
        //Will create the fishing port which will allow for fishing
        if (upgradeInformation[0].foodCost <= gameScript.foodAmount && upgradeInformation[0].woodCost <= gameScript.woodAmount)
        {
            fishingUpgrade = true;
            fishShack.SetActive(true);
            Player.instance.BuildFishingPort();
            gameScript.UpdateFoodUI(-upgradeInformation[0].foodCost);
            gameScript.UpdateWoodUI(-upgradeInformation[0].woodCost);
            ResourceGained.instance.LoseResource(foodRequired, 0);
            ResourceGained.instance.LoseResource(woodRequired, 2);
            TextRecord.instance.PostMessage("You have built a fishing port!");
        }
        else
        {
            TextRecord.instance.PostMessage("You do not have the required amount to build this!");
        }
    }

    private void ActionPointUpgrade()
    {
        if (upgradeInformation[1].foodCost <= gameScript.foodAmount && upgradeInformation[1].woodCost <= gameScript.woodAmount)
        {
            actionPointUpgrade = true;
            Player.instance.ActionPointUpgrade();
            gameScript.UpdateFoodUI(-upgradeInformation[1].foodCost);
            gameScript.UpdateWoodUI(-upgradeInformation[1].woodCost);
            ResourceGained.instance.LoseResource(foodRequired, 0);
            ResourceGained.instance.LoseResource(woodRequired, 2);
            TextRecord.instance.PostMessage("You feel as though you can accomplish more everyday!");

        }
        else
        {
            TextRecord.instance.PostMessage("You do not have the required amount to build this!");

        }
    }

    private void ScarfUpgrade()
    {
        if (upgradeInformation[2].foodCost <= gameScript.foodAmount && upgradeInformation[2].woodCost <= gameScript.woodAmount)
        {
            scarfUpgrade = true;
            TextRecord.instance.PostMessage("You feel much warmer. You are not effected as much by temperature!");
            gameScript.UpdateFoodUI(-upgradeInformation[2].foodCost);
            gameScript.UpdateWoodUI(-upgradeInformation[2].woodCost);
            ResourceGained.instance.LoseResource(foodRequired, 0);
            ResourceGained.instance.LoseResource(woodRequired, 2);

        }
        else
        {
            TextRecord.instance.PostMessage("You do not have the required amount to build this!");

        }
    }

    private void FishUpgradeUICreate()
    {
        var upgrade = Instantiate(_upgradeUI, parent.position + new Vector3(0, 0.8f, 0), Quaternion.identity, parent);
        var foodText = upgrade.transform.Find("FoodAmountRequired").GetComponent<Text>();
        var woodText = upgrade.transform.Find("WoodAmountRequired").GetComponent<Text>();
        var upgradeButton = upgrade.transform.Find("Button").GetComponent<Button>();
        var image = upgrade.transform.Find("UpgradeImageIcon").GetComponent<Image>();

        foodText.text = upgradeInformation[0].foodCost.ToString();
        woodText.text = upgradeInformation[0].woodCost.ToString();
        image.sprite = upgradeInformation[0].icon;
        upgradeButton.onClick.AddListener(CreateFishingPort);
    }

    private void ActionPointUICreate()
    {
        var upgrade = Instantiate(_upgradeUI, parent.position + new Vector3(0, 0.2f, 0), Quaternion.identity, parent);
        var foodText = upgrade.transform.Find("FoodAmountRequired").GetComponent<Text>();
        var woodText = upgrade.transform.Find("WoodAmountRequired").GetComponent<Text>();
        var upgradeButton = upgrade.transform.Find("Button").GetComponent<Button>();
        var image = upgrade.transform.Find("UpgradeImageIcon").GetComponent<Image>();


        foodText.text = upgradeInformation[1].foodCost.ToString();
        woodText.text = upgradeInformation[1].woodCost.ToString();
        image.sprite = upgradeInformation[1].icon;

        upgradeButton.onClick.AddListener(ActionPointUpgrade);
    }

    private void ScarfUICreate()
    {
        var upgrade = Instantiate(_upgradeUI, parent.position + new Vector3(0, -0.4f, 0), Quaternion.identity, parent);
        var foodText = upgrade.transform.Find("FoodAmountRequired").GetComponent<Text>();
        var woodText = upgrade.transform.Find("WoodAmountRequired").GetComponent<Text>();
        var upgradeButton = upgrade.transform.Find("Button").GetComponent<Button>();
        var image = upgrade.transform.Find("UpgradeImageIcon").GetComponent<Image>();


        foodText.text = upgradeInformation[2].foodCost.ToString();
        woodText.text = upgradeInformation[2].woodCost.ToString();
        image.sprite = upgradeInformation[2].icon;

        upgradeButton.onClick.AddListener(ScarfUpgrade);
    }


}
