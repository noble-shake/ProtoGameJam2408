using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum GuestState
{ 
    InOrder,
    Wait,
    Exit,
    EntryExit,
    Entry,
    Drink,
}

public class Penguine : MonoBehaviour
{
    int guestID;
    public int queOrder;

    public GuestState currentState;
    [SerializeField] StorageItem WantMenu;

    float currentWait;
    [SerializeField] float waitTime;

    float currentDrinkTime;
    [SerializeField] float drinkTime;

    int EntryNumber;
    Entry AllocatedEntry;

    [SerializeField] float speed;

    [SerializeField] Transform CharSpr;
    [SerializeField] GameObject emojiObject;
    [SerializeField] Image emojiBG;
    [SerializeField] Image emojiIcon;
    [SerializeField] Image headImage;

    public int identify { get { return guestID; } set { guestID = value; } }
    public StorageItem menu {get {return WantMenu;} set { WantMenu = value; } }

    private void OnEnable()
    {
        if(headImage.gameObject.activeSelf) headImage.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        emojiBG.fillAmount = 0;
        emojiObject.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GuestState.InOrder;
        currentWait = waitTime;
        currentDrinkTime = drinkTime;
        emojiBG.fillAmount = 0f;
        emojiObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
    }

    private void Behaviour()
    {
        switch (currentState)
        { 
            case GuestState.InOrder:
                LineMove();
                break;
            case GuestState.Wait:
                WaitCheck();
                break;
            case GuestState.Entry:
                break;
            case GuestState.Drink:
                DrinkCheck();
                break;

        }
    }

    public void LineMove()
    {

        Vector2 targetVec = GuestManager.instance.StartPoint.transform.position + new Vector3(queOrder * 0.9f, 0f, 0f);

        if (Vector2.Distance(transform.position, targetVec) < 0.005f)
        {
            if (queOrder == 0)
            {
                currentState = GuestState.Wait;
                emojiObject.gameObject.SetActive(true);
                emojiIcon.sprite = GameManager.instance.getImage((dynamicSprites)System.Enum.Parse(typeof(dynamicSprites), WantMenu.ToString()));
                GameManager.instance.GuestOn = this;
                return;
            } 

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetVec, Time.deltaTime * speed);
    }

    public void WaitCheck()
    {
        currentWait -= Time.deltaTime;
        emojiBG.fillAmount += Time.deltaTime / waitTime;
        if (currentWait < 0) 
        {
            AllocatedEntry = FindEntryPoint.instance.noEntry;
            currentState = GuestState.EntryExit;
            currentWait = waitTime;
            GuestManager.instance.OrderEnd();
            GameManager.instance.WaitExit();
            emojiBG.fillAmount = 1f;
            StartCoroutine(EmojiPop(dynamicSprites.angry));
            ExitCheck();
        }
    }

    public void WaitReset()
    {
        currentWait = waitTime;
        emojiBG.fillAmount = 0f;
    }

    public void DrinkCheck()
    {
        currentDrinkTime -= Time.deltaTime;
        if (currentDrinkTime < 0)
        {
            AllocatedEntry.AllocatedTable.gameObject.SetActive(false);
            currentState = GuestState.EntryExit;
            ExitCheck();
            currentDrinkTime = drinkTime;
            FindEntryPoint.instance.ReturnTable(EntryNumber);
        }
    }

    public void ExitCheck()
    {
        StartCoroutine(ExitCoroutine());
    }

    public bool EntryAllocate()
    {
        emojiObject.gameObject.SetActive(false);
        headImage.sprite = GameManager.instance.getImage((dynamicSprites)System.Enum.Parse(typeof(dynamicSprites), WantMenu.ToString()));
        headImage.gameObject.SetActive(true);

        EntryNumber = FindEntryPoint.instance.AllocateTable();

        if (EntryNumber == -1)
        {
            StartCoroutine(EmojiPop(dynamicSprites.smile));
            ExitCheck();
            return false;
        }
        else
        {
            AllocatedEntry = FindEntryPoint.instance.entries[EntryNumber];
            StartCoroutine(EntryCoroutine());
            return true;
        }

    }

    public IEnumerator EntryCoroutine()
    {
        Queue<Vector2> queVector = new Queue<Vector2>();
        for (int idx = 0; idx < AllocatedEntry.Path.Count; idx++)
        { 
            queVector.Enqueue(AllocatedEntry.Path[idx].transform.position);
        }
        queVector.Enqueue(AllocatedEntry.transform.position);

        Vector2 Road = queVector.Dequeue();

        bool flag = false;
        float sightX = transform.position.x;
        while (flag == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Road, Time.deltaTime * speed);

            if (sightX < transform.position.x)
            {
                CharSpr.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (sightX > transform.position.x)
            {
                CharSpr.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            yield return null;
            sightX = transform.position.x;
            if (Vector2.Distance((Vector2)transform.position, Road) < 0.005f)
            {

                if (queVector.Count == 0)
                {
                    flag = true;
                }
                else
                {
                    Road = queVector.Dequeue();
                }
            }
        }

        yield return null;
        currentState = GuestState.Drink;
        headImage.gameObject.SetActive(false);
        //Entry Set Drink.
        AllocatedEntry.AllocatedTable.sprite = GameManager.instance.getImage((dynamicSprites)System.Enum.Parse(typeof(dynamicSprites), WantMenu.ToString()));
        AllocatedEntry.AllocatedTable.gameObject.SetActive(true);
        // AllocatedEntry.gameObject.SetActive(true);  

        StartCoroutine(EmojiPop(dynamicSprites.smile));
    }

    public IEnumerator EmojiPop(dynamicSprites _spr)
    { 
        if(!emojiObject.activeSelf) emojiObject.SetActive(true);


        emojiIcon.sprite = GameManager.instance.getImage(_spr);

        yield return new WaitForSeconds(1f);

        emojiObject.SetActive(false);
    }


    public IEnumerator ExitCoroutine()
    {
        Queue<Vector2> queVector = new Queue<Vector2>();
        for (int idx = AllocatedEntry.Path.Count-1; idx > 0; idx--)
        {
            queVector.Enqueue(AllocatedEntry.Path[idx].transform.position);
        }

        queVector.Enqueue(GuestManager.instance.ExitDoorPoint.transform.position);
        int doorEffectCount = queVector.Count;
        queVector.Enqueue(GuestManager.instance.ExitPoint.transform.position);

        Vector2 Road = queVector.Dequeue();

        bool flag = false;

        float sightX = transform.position.x;


        while (flag == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Road, Time.deltaTime * speed);

            if (sightX < transform.position.x)
            {
                CharSpr.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (sightX > transform.position.x)
            {
                CharSpr.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            yield return null;
            sightX = transform.position.x;
            if (Vector2.Distance((Vector2)transform.position, Road) < 0.005f)
            {
                if (queVector.Count == doorEffectCount)
                {
                    GuestManager.instance.OpenExitDoor();
                }

                if (queVector.Count == 0)
                {
                    flag = true;
                }
                else
                {
                    Road = queVector.Dequeue();
                }
            }
        }
        yield return null;
        currentState = GuestState.Exit;
        CharSpr.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.SetActive(false);
    }
}
