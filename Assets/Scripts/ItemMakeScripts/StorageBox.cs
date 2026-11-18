using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;


public enum StorageItem
{
    hot_ame,
    ice_ame,
    hot_latte,
    ice_latte,
    ice_tea,
}



public class StorageBox : MonoBehaviour
{
    public static StorageBox instance;

    [SerializeField] float displayTime;
    float curTime;

    Juice juiceObject;
    public bool Upgraded;

    int MaxFillAmount;

    [SerializeField] JuiceStock HotAmeObj;
    [SerializeField] JuiceStock IceAmeObj;
    [SerializeField] JuiceStock HotLatteObj;
    [SerializeField] JuiceStock IceLatteObj;
    [SerializeField] JuiceStock IceTeaObj;
    
    Sprite HotAmeSpr;
    Sprite IceAmeSpr;
    Sprite HotLatteSpr;
    Sprite IceLatteSpr;
    Sprite IceTeaSpr;

    Dictionary<string, int> Stored;


    public void StoreExtend()
    {
        if (Upgraded == true) return;
        Upgraded = true;
        MaxFillAmount = 50;
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
    }

    public void Start()
    {
        MaxFillAmount = 30;
        HotAmeSpr = GameManager.instance.getImage(dynamicSprites.hot_ame);
        IceAmeSpr = GameManager.instance.getImage(dynamicSprites.ice_ame);
        HotLatteSpr = GameManager.instance.getImage(dynamicSprites.hot_latte);
        IceLatteSpr = GameManager.instance.getImage(dynamicSprites.ice_latte);
        IceTeaSpr = GameManager.instance.getImage(dynamicSprites.ice_tea);

        Stored = new Dictionary<string, int>();
        

        int count = System.Enum.GetNames(typeof(StorageItem)).Length;


        for (int idx = 0; idx < count; idx++)
        {
            Stored[((StorageItem)idx).ToString()] = 0;
        }
    }


    public bool GetOrder(StorageItem _item)
    {
        string target = _item.ToString();

        if (Stored[target] != 0)
        {
            Stored[target]--;

            switch (_item)
            {
                case StorageItem.hot_ame:
                    HotAmeObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_ame:
                    IceAmeObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.hot_latte:
                    HotLatteObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_latte:
                    IceLatteObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_tea:
                    IceTeaObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
            }

            return true;
        }
        return false;
    }

    public void SetStock(StorageItem _item)
    {
        string target = _item.ToString();
        if (Stored[target] < MaxFillAmount)
        {
            Stored[target]++;

            switch (_item)
            {
                case StorageItem.hot_ame:
                    if (HotAmeObj.gameObject.activeSelf == false) HotAmeObj.gameObject.SetActive(true);
                    HotAmeObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_ame:
                    if (IceAmeObj.gameObject.activeSelf == false) IceAmeObj.gameObject.SetActive(true);
                    IceAmeObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.hot_latte:
                    if (HotLatteObj.gameObject.activeSelf == false) HotLatteObj.gameObject.SetActive(true);
                    HotLatteObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_latte:
                    if (IceLatteObj.gameObject.activeSelf == false) IceLatteObj.gameObject.SetActive(true);
                    IceLatteObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
                case StorageItem.ice_tea:
                    if (IceTeaObj.gameObject.activeSelf == false) IceTeaObj.gameObject.SetActive(true);
                    IceTeaObj.stock = (float)Stored[target] / MaxFillAmount;
                    break;
            }
        }


        
    }

    private void OnMouseOver()
    {
        curTime -= Time.deltaTime;

        if (curTime < 0f)
        {
            curTime = 0f;
        }

        //display On.
    }

    private void OnMouseExit()
    {
        curTime = displayTime;

        //display off.
    }


    private void MouseDragCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (juiceObject != null)
            {
                string work = juiceObject.menuType.ToString();

                StorageItem stock;
                if (System.Enum.TryParse(work, out stock))
                {
                    SetStock(stock);
                    CookingStation.instance.StationClear();
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void Update()
    {
        MouseDragCheck();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("WorkIn"))
        {
            juiceObject = collision.GetComponent<Juice>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("WorkIn"))
        {
            juiceObject = collision.GetComponent<Juice>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("WorkIn"))
        {
            MouseDragCheck();
            juiceObject = null;
        }
    }
}
