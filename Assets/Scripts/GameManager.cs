using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int dayNum = 1;

    public Text day;
    public Text woodCounter;
    public Text tempCounter;
    public Text foodCounter;

    [SerializeField] private Text _noPlayerDeck;
    [SerializeField] private Text _noPlayerGraveyard;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject endOfDayScreen;

    public int woodAmount;
    public float tempAmount;
    public int foodAmount;
    public int totalActionPoints;

    public bool hasDrawn = false;

    [SerializeField] public GameObject[] cards;
    [SerializeField] private GameObject[] randomCardEvents;
    [SerializeField] private DailyEvents dailyEventScript;
    [SerializeField] private Transform cardSpawn;

    [SerializeField] private Sprite actionPointsFull;
    [SerializeField] private Sprite actionPointsEmpty;
    [SerializeField] private Image[] actionPointsImage;

    [SerializeField] private Animator anim;
    [SerializeField] private Text endOfDay;

    [SerializeField] private Text _deathText;
    //[SerializeField] private GameObject[] actionPointsInterfaceUI;

    CardManagement cardScript;
    Player playerScript;
    ShopManager shopScript;
    SoundManager soundScript;
    DailyEvents _eventScript;
    Upgrades _upgradeScript;
    // Start is called before the first frame update
    void Start()
    {
        cardScript = GameObject.Find("CardManager").GetComponent<CardManagement>();
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        shopScript = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        soundScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _upgradeScript = GameObject.Find("Canvas").transform.Find("UpgradeInterface").GetComponent<Upgrades>();

        _eventScript = GetComponent<DailyEvents>();
        dailyEventScript = gameObject.GetComponent<DailyEvents>();
        day.text = "Day: " + dayNum.ToString();
        totalActionPoints = playerScript.actionPoints;
        woodCounter.text = woodAmount.ToString();
        tempCounter.text = tempAmount.ToString();
        foodCounter.text = foodAmount.ToString();
        _noPlayerDeck.text = playerScript.playerDeck.Count.ToString();
        _noPlayerGraveyard.text = playerScript.playerGraveyard.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _noPlayerDeck.text = playerScript.playerDeck.Count.ToString();
        _noPlayerGraveyard.text = playerScript.playerGraveyard.Count.ToString();
    }

    public void UpdateActionPointUI(int currentActionPoints)
    {
        for (int i = 0; i < actionPointsImage.Length; i++)
        {
            if(i < currentActionPoints)
            {
                actionPointsImage[i].sprite = actionPointsFull;
            }
            else
            {
                actionPointsImage[i].sprite = actionPointsEmpty;
                
            }
        }
    }

    public void RefreshActionPoints(int currentActionPoints, int playerMaximumActionPoints, int actionPointsToRefresh)
    {
        if(currentActionPoints + actionPointsToRefresh > playerMaximumActionPoints)
        {
            playerScript.actionPoints = playerMaximumActionPoints;
        }
        else
        {
            playerScript.actionPoints += actionPointsToRefresh;
        }

        UpdateActionPointUI(actionPointsToRefresh);
    }

    public void NextDay()
    {
        dayNum++;
        day.text = "Day: " + dayNum.ToString();
        endOfDay.text = "Day: " + dayNum.ToString();
        if(cardScript._hasSaltedFood == false)
            foodAmount -= Random.Range(1, 2);
        else
            TextRecord.instance.PostMessage("You managed to save some of your food due to salting");

        if (foodAmount <= 0)
        {
            foodAmount = 0;
        }
        foodCounter.text = foodAmount.ToString();

        if(cardScript._hasSaltedFood == false)
        {
            if (_eventScript.HeatWaveCheck())
            {
                _eventScript.HeatWaveActivate();
                _eventScript.HeatWaveOff();
            }
        }

        ShopManager.instance.EndOfDay();
        GameEffects.ClearIcons_Static();
        if(cardScript.IsTrapActive())
        {
            GameEffects.DisplayIcon_Static(4);
        }

        Player.instance.ClearHand();

        playerScript.actionPoints = playerScript.maxActionPoints;
        StartCoroutine(EndOfDayScene());
        cardScript.SetSaltedFood();

        UpdateActionPointUI(playerScript.maxActionPoints);    
    }

    IEnumerator EndOfDayScene()
    {
        if(!GameOver())
        {
            
            endOfDayScreen.SetActive(true);
            anim.SetBool("endOfDay", true);
            yield return new WaitForSeconds(4f);
            anim.SetBool("endOfDay", false);
            endOfDayScreen.SetActive(false);
            NewDayEvents();
        }
        else
        {
            gameOverScreen.SetActive(true);
            if(foodAmount <= 0)
            {
                DeathMessage("You died through the night of starvation");
            }
            else if(tempAmount <= 0)
            {
                DeathMessage("You died due to the elements");
            }
            else
            {
                DeathMessage("Life is cruel");
            }
        }
        
    }

    private void DeathMessage(string deathMessage)
    {
        _deathText.text = deathMessage;
    }

    public void CardDraw()
    {
        StartCoroutine(DrawCards());

    }

    IEnumerator DrawCards()
    {
        if (!hasDrawn)
        {
            Vector3 offset = new Vector3(1, 0, 0);
            float rotationOffset = 16f;
            float radius = 4f;

            for (int i = 0; i < Player.instance.drawAmount; i++)
            {

                //float angle = 180f / (float)Player.instance.drawAmount;
                Player.instance.CardDraw(cardSpawn, offset, rotationOffset, Player.instance.drawAmount);
                offset.x += 3f;
                //offset.y += 0.5f;
                int randSound = Random.Range(1, 3);
                soundScript.PlaySound(randSound);
                rotationOffset -= 16f;
                yield return new WaitForSeconds(0.3f);
            }

            hasDrawn = true;
        }
        else
        {
            TextRecord.instance.PostMessage("You have already drawn for today");
        }

        Player.instance.drawAmount = Player.instance.defaultHandSize;
    }

    private bool GameOver()
    {
        if(foodAmount <= 0 || tempAmount <= 0)
        {
            return true;
        }            
        else
        {
            return false;
        }
        
    }

    private void NewDayEvents()
    {

        hasDrawn = false;

        if(playerScript.CantDrawMore())
        {
            playerScript.ResetDeck();
        }

        int randNo = Random.Range(0, dailyEventScript.dailyEventCards.Length);
        dailyEventScript.cardPick(randNo);
        playerScript.actionPoints = playerScript.maxActionPoints;
        cardScript.CheckTraps();
    }

    public void GameShop()
    {
        shopScreen.SetActive(true);
        shopScript.BuildShopFront();
    }

    public void UpdateFoodUI(int foodToUpdate)
    {

        foodAmount += foodToUpdate;
        foodCounter.text = foodAmount.ToString();
    }

    public void UpdateWoodUI(int woodToUpdate)
    {
        woodAmount += woodToUpdate;
        woodCounter.text = woodAmount.ToString();
    }

    public void UpdateTempUI(float tempToUpdate)
    {
        
        if(_upgradeScript.scarfUpgrade == false)
        {

            tempAmount += tempToUpdate;
            tempCounter.text = tempAmount.ToString();
        }
        else
        {
            tempAmount += tempToUpdate + _upgradeScript.scarfWarmth;
            tempCounter.text = tempAmount.ToString();

            TextRecord.instance.PostMessage("Your scarf managed to save reduce your cold impact");
        }
        
    }

}
