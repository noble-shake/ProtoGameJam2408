using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum enumMenu
{
    Americano,
    IceAmericano,
    IceTea,
    CafeLatte,
    IceCafeLatte,
}


public class GuestManager : MonoBehaviour
{
    public static GuestManager instance;
    int poolingIndex = 50;

    [SerializeField] Door EntraceDoor;
    [SerializeField] Door ExitDoor;
    [SerializeField] public GameObject StartPoint;
    [SerializeField] public GameObject EndPoint;
    [SerializeField] public GameObject EntracePoint;
    [SerializeField] public GameObject ExitPoint;

    [SerializeField] Transform GuestGroup;
    [SerializeField] Penguine GuestPrefab;
    List<Penguine> Pool;
    public List<Penguine> OrderWaitingGuests;

    [SerializeField] float currentSpawnTimer;
    [SerializeField] float spawnTimer = 5f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(instance);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Pool = new List<Penguine>();
        OrderWaitingGuests = new List<Penguine>();
        PenguinePooling();
        currentSpawnTimer = spawnTimer;
    }

    private void PenguinePooling()
    {
        for (int idx = 0; idx < poolingIndex; idx++)
        {
            Penguine guest = Instantiate(GuestPrefab, GuestGroup);
            guest.gameObject.SetActive(false);
            Pool.Add(guest);
        }
    }

    public void SpawnGuest()
    {

        if (OrderWaitingGuests.Count > 3) return;
        // if (EntraceDoor.idle == false) return;

        currentSpawnTimer -= Time.deltaTime;

        if (currentSpawnTimer > 0f) return;

        if (currentSpawnTimer < 0f)
        {
            EntraceDoor.OpenAnim();
            currentSpawnTimer = spawnTimer;
        }

        
        for (int idx = 0; idx < poolingIndex; idx++)
        {
            if (Pool[idx].gameObject.activeSelf == false)
            {
                Penguine go = Pool[idx];
                go.gameObject.SetActive(true);
                go.gameObject.transform.position = EntracePoint.transform.position;
                go.menu = (enumMenu)Random.Range(0, 5);
                go.queOrder = OrderWaitingGuests.Count;
                OrderWaitingGuests.Add(go);
                go.currentState = GuestState.InOrder;

                return;
            }
        }
    }

    public void OrderEnd()
    {
        OrderWaitingGuests.RemoveAt(0);
        foreach (Penguine go in OrderWaitingGuests)
        {
            go.queOrder--;
        }
    }


    // Update is called once per frame
    void Update()
    {
        SpawnGuest();
    }
}
