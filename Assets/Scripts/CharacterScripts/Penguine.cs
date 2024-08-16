using System.Collections;
using System.Collections.Generic;
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
    enumMenu WantMenu;

    float currentWait;
    [SerializeField] float waitTime;

    float currentDrinkTime;
    [SerializeField] float drinkTime;

    Entry AllocatedEntry;

    [SerializeField] float speed;

    [SerializeField] GameObject emojiObject;
    [SerializeField] Image emojiBG;
    [SerializeField] Image emojiIcon;

    public int identify { get { return guestID; } set { guestID = value; } }
    public enumMenu menu {get {return WantMenu;} set { WantMenu = value; } }

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
                // Path Going
                break;
            case GuestState.Drink:
                break;

        }
    }

    public void LineMove()
    {

        Vector2 targetVec = GuestManager.instance.StartPoint.transform.position + new Vector3(queOrder * 0.8f, 0f, 0f);

        if (Vector2.Distance(transform.position, targetVec) < 0.005f)
        {
            if (queOrder == 0)
            {
                currentState = GuestState.Wait;
                emojiObject.gameObject.SetActive(true);
                return;
            } 

            //if (GuestManager.instance.OrderWaitingGuests.Count < queOrder)
            //{
            //    queOrder--;
            //}

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
            // emoticon. unHappy
            // AllocatedEntry = GuestManager.instance;
            AllocatedEntry = FindEntryPoint.instance.noEntry;
            currentState = GuestState.EntryExit;
            currentWait = waitTime;
            GuestManager.instance.OrderEnd();
            emojiBG.fillAmount = 1f;
            ExitCheck();
        }
    }

    public void DrinkCheck()
    {
        currentDrinkTime -= Time.deltaTime;
        if (currentDrinkTime < 0)
        {
            // emoticon. happy
            currentState = GuestState.EntryExit;
            ExitCheck();
            currentDrinkTime = drinkTime;
        }
    }

    public void ExitCheck()
    {
        StartCoroutine(ExitCoroutine());
    }

    public IEnumerator ExitCoroutine()
    {
        Queue<Vector2> queVector = new Queue<Vector2>();
        for (int idx = AllocatedEntry.Path.Count -1; idx > 0; idx--)
        {
            queVector.Enqueue(AllocatedEntry.Path[idx].transform.position);
        }
        queVector.Enqueue(GuestManager.instance.ExitPoint.transform.position);

        Vector2 Road = queVector.Dequeue();
        if (queVector.Count == 0)
        {
            while (true)
            {
                transform.position = Vector2.MoveTowards(transform.position, Road, Time.deltaTime * speed);

                yield return null;
                if (Vector2.Distance(transform.position, Road) < 0.005f) break;
            }
        }
        else
        {
            while (queVector.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Road, Time.deltaTime * speed);

                yield return null;
                if (Vector2.Distance(transform.position, Road) < 0.005f)
                {
                    Road = queVector.Dequeue();
                }
            }
        }

        yield return null;
        currentState = GuestState.Exit;
        gameObject.SetActive(false);
    }
}
