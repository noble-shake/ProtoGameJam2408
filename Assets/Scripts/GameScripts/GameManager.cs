using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum dynamicSprites
{ 
    smile,
    angry,
    coffeebean,
    ice,
    poweder,
    milk,
    hot_ame,
    ice_ame,
    hot_latte,
    ice_latte,
    ice_tea,
    fail,
    unknown,
}

public enum enumWorkResult
{
    coffeebean,
    ice,
    poweder,
    milk,
    hot_ame,
    ice_ame,
    ice_tea,
    hot_latte,
    ice_latte,
    unknown,
    fail,
}

public enum enumBasicWork
{
    coffeebean,
    ice,
    poweder,
    milk,
}


public enum enumStore
{ 
    AirCond1,
    AirCond2,
    Fan,
    StoreUp,
}

public enum ScoreDivide
{ 
    HOT,
    COLD,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    [SerializeField] Transform CanvasTrs;
    [SerializeField] int Money;
    [SerializeField] int Score;
    [SerializeField] int GuestCount;
    [SerializeField] int makeJuiceCount;
    [SerializeField] float TimeValue;

    [SerializeField] TMP_Text MoneyText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text TimeText;
    [SerializeField] Image TimeFillImage;

    [SerializeField] Toggle BtnRecipe;
    [SerializeField] Toggle BtnStore;


    [Header("Prefab")]
    [SerializeField] Recipe RecipePrefab;
    Recipe RecipeIns;
    [SerializeField] Store StoreDisplayPrefab;
    Store StoreDisplayIns;
    [SerializeField] EndScene EndScenePrefab;

    [SerializeField] public Sprite[] images;
    [SerializeField] GameObject AirCond1Object;
    [SerializeField] GameObject AirCond2Object;
    [SerializeField] GameObject FanObject;

    [Header("Data")]
    Penguine curInOrderGuest;
    [SerializeField] float OrderDelay;
    [SerializeField] float currentDelay;
    [SerializeField] float TimeLimit;
    float GameTime;

    [Header("StoreManage")]
    public float buy1Remain;
    public float buy2Remain;
    public float buy3Remain;
    public bool StoreUpgrade;

    public bool TutorialCheck;
    bool EndCheck;

    public Penguine GuestOn { set { curInOrderGuest = value; } }


    public void JuiceCounting()
    {
        makeJuiceCount++;
    }


    public Sprite getImage(dynamicSprites _spr)
    {
        return images[(int)_spr];
    }

    public void WaitExit()
    {
        curInOrderGuest = null;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }

        TutorialCheck = true;
    }


    private void TimeSpanning(float _time)
    {
        if (_time < 0) _time = 0f;

        TimeFillImage.fillAmount = 1f - (_time / 1080f);
        // 1m30s == 1h (90s),  09 -> 21 (12hour) =>  1080f

        int currentHour = 11 - (int)_time / 90;
        int displayHour = 9 + currentHour;

        string changedText = displayHour.ToString("D2");

        if (_time == 0f) changedText = "21";

        TimeText.text = $"{changedText}: 00";

    }

    public void GameEndCheck()
    {
        if (GameTime != 0f) return;
        if (GuestManager.instance.PenguineEmptyCheck() == false) return;
        if (EndCheck == true) return;

        EndCheck = true;

        EndingScene end = Score > 3000 ? EndingScene.GOOD : EndingScene.BAD;
        EndScene go = Instantiate(EndScenePrefab, CanvasTrs);
        go.SetEnding(end, Score, Money, makeJuiceCount, GuestCount);

    }

    public IEnumerator FadeOut()
    { 
        yield return null;
    }


    private void FixedUpdate()
    {
        if (TutorialCheck) return;

        GameTime -= Time.fixedDeltaTime;
        TimeSpanning(GameTime);

        if (GameTime < 0f)
        {
            GameTime = 0f;
        }
        else
        {
            GuestManager.instance.SpawnGuest();
        }

        GameEndCheck();


        buy1Remain -= Time.fixedDeltaTime;
        buy2Remain -= Time.fixedDeltaTime;
        buy3Remain -= Time.fixedDeltaTime;

        if (buy1Remain < 0f)
        {
            if(AirCond1Object.activeSelf) AirCond1Object.SetActive(false);
            buy1Remain = 0f;
        }

        if (buy2Remain < 0f)
        {
            if (AirCond2Object.activeSelf) AirCond2Object.SetActive(false);
            buy2Remain = 0f;
        }

        if (buy3Remain < 0f)
        {
            if (FanObject.activeSelf) FanObject.SetActive(false);
            buy3Remain = 0f;
        }
    }

    private void Start()
    {
        EndCheck = false;
        Money = 0;
        Score = 0;
        makeJuiceCount = 0;
        GuestCount = 0;

        GameTime = 1080f; 
        // 30m -> 60 X 30 -> 1800f
        // 1m30s == 1h (90s),  09 -> 21 (12hour) =>  1080f

        MoneyText.text = Money.ToString();
        ScoreText.text = Score.ToString();

        BtnRecipe.onValueChanged.AddListener((eventData) => OnRecipeClick(eventData));
        BtnStore.onValueChanged.AddListener((eventData) => OnStoreClick(eventData));
    }

    public void OnRecipeClick(bool data)
    {
        if (data)
        {
            RecipeIns = Instantiate(RecipePrefab, CanvasTrs);
            RecipeIns.SetToggleParent(BtnRecipe);

            if (StoreDisplayIns != null)
            {
                Destroy(StoreDisplayIns);
                BtnStore.GetComponent<Toggle>().isOn = false;
            } 

        }
        else
        {
            Destroy(RecipeIns.gameObject);
        }
    }

    public void OnStoreClick(bool data)
    {
        if (data)
        {
            StoreDisplayIns = Instantiate(StoreDisplayPrefab, CanvasTrs);
            StoreDisplayIns.SetToggleParent(BtnStore);

            if (RecipeIns != null)
            {
                Destroy(RecipeIns);
                BtnRecipe.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            Destroy(StoreDisplayIns.gameObject);
        }
    }

    public void BuyObject(enumStore _store)
    {
        switch (_store)
        {
            case enumStore.StoreUp:
                if (StoreUpgrade == true) return;
                if (Money < 5000) return;
                StoreUpgrade = true;
                StorageBox.instance.StoreExtend();
                MoneyText.text = Money.ToString();
                
                break;
            case enumStore.AirCond1:
                if (Money < 3500) return;
                Money -= 3500;
                buy1Remain = 180f;
                MoneyText.text = Money.ToString();
                AirCond1Object.SetActive(true);
                break;
            case enumStore.AirCond2:
                if (Money < 3500) return;
                Money -= 3500;
                buy2Remain = 180f;
                MoneyText.text = Money.ToString();
                AirCond2Object.SetActive(true);
                break;
            case enumStore.Fan:
                if (Money < 2000) return;
                Money -= 2000;
                buy3Remain = 180f;
                MoneyText.text = Money.ToString();
                FanObject.SetActive(true);
                break;
        }

    }

    private void Update()
    {
        Debug.Log($"{Score} SCORE PLEASE");

        if(currentDelay != 0) currentDelay -= Time.deltaTime;
        if (currentDelay < 0) currentDelay = 0f;

        OrderCheck();
    }

    private void OrderCheck()
    {
        if (currentDelay > 0f) return;

        if (Human.instance.playerState == PlayerState.ORDER && curInOrderGuest != null)
        {
            if (StorageBox.instance.GetOrder(curInOrderGuest.menu) == true)
            {
                StorageItem _item = curInOrderGuest.menu;

                curInOrderGuest.currentState = GuestState.Entry;
                curInOrderGuest.WaitReset();
                bool entryCheck = curInOrderGuest.EntryAllocate();
                curInOrderGuest = null;

                currentDelay = OrderDelay;
                GuestManager.instance.OrderEnd();
                

                // Game Result
                MoneyUp(_item, entryCheck);

                switch (_item)
                {
                    case StorageItem.hot_ame:
                    case StorageItem.hot_latte:
                        ScoreUp(ScoreDivide.HOT, entryCheck);
                        break;
                    case StorageItem.ice_ame:
                    case StorageItem.ice_latte:
                    case StorageItem.ice_tea:
                        ScoreUp(ScoreDivide.COLD, entryCheck);
                        break;
                }
                GuestCount++;
            }
        }
    }

    public void MoneyUp(StorageItem _item, bool entryCheck)
    {
        int sellingItem = 0;
        switch (_item)
        {
            case StorageItem.hot_ame:
                sellingItem = 200;
                break;
            case StorageItem.ice_ame:
                sellingItem = 200;
                break;
            case StorageItem.hot_latte:
                sellingItem = 350;
                break;
            case StorageItem.ice_latte:
                sellingItem = 350;
                break;
            case StorageItem.ice_tea:
                sellingItem = 300;
                break;
        }

        Money += sellingItem;
        MoneyText.text = Money.ToString();
    }

    public void ScoreUp(ScoreDivide _div, bool entryCheck)
    {
        int earned = 0;

        if (entryCheck == true)
        {
            earned = 10;
        }
        else
        {
            earned = 5;
        }

        switch(_div)
        {
            case ScoreDivide.HOT:                
                break;
            case ScoreDivide.COLD:
                earned += 5;
                break;
        }

        if (buy1Remain > 0f)
        {
            earned += 5;
        }
        if (buy1Remain > 0f)
        {
            earned += 5;
        }
        if (buy1Remain > 0f)
        {
            earned += 5;
        }

        Score += earned;
        ScoreText.text = Score.ToString();
    }
}
