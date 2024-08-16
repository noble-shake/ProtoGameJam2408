using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum enumWorkResult
{ 
    CoffeBean,
    Ice,
    Powder,
    Milk,
    Americano,
    IceAmericano,
    IceTea,
    CafeLatte,
    IceCafeLatte,
    Unknown,
    Failed,
}

public enum enumBasicWork
{
    CoffeBean,
    Ice,
    Powder,
    Milk,
}



public class WorkStation : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    enumWorkResult CurrentMenu;
    [SerializeField] Transform CanvasTrs;
    [SerializeField] TMP_Text Result;
    [SerializeField] Image ResultObject;
    List<int> recipe; // 0 : Bean, 1 : Ice, 2 : Mixer, 3 : Milk
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.CompareTag("WorkIn"))
        {
            WorkInteraction go = eventData.pointerDrag.gameObject.GetComponent<WorkInteraction>();

            
            if (recipe.Count < 3)
            {
                recipe.Add(go.workID);
                CurrentMenu = MixRecipe((enumBasicWork)System.Enum.Parse(typeof(enumBasicWork), System.Enum.GetName(typeof(enumBasicWork), go.workID)));
                
            }

            ResultObject.gameObject.SetActive(true);
            Human.instance.StateSwithcing(PlayerState.ORDER);
        }
    }

    private enumWorkResult MixRecipe(enumBasicWork target)
    {
        int CoffeBean = 0;
        int Ice = 0;
        int Powder = 0;
        int Milk = 0;

        if (recipe.Count == 1)
        {
            switch (target)
            { 
                case enumBasicWork.CoffeBean:
                    return enumWorkResult.CoffeBean;
                case enumBasicWork.Ice:
                    return enumWorkResult.CoffeBean;
                case enumBasicWork.Powder:
                    return enumWorkResult.CoffeBean;
                case enumBasicWork.Milk:
                    return enumWorkResult.CoffeBean;
            }
        }

        for (int idx = 0; idx < recipe.Count; idx++)
        {
            switch (recipe[idx])
            {
                case 0:
                    CoffeBean++;
                    break;
                case 1:
                    Ice++;
                    break;
                case 2:
                    Powder++;
                    break;
                case 3:
                    Milk++;
                    break;
            }
        }

        if (recipe.Count == 3)
        {
            if (CoffeBean == 3)
            {
                return enumWorkResult.Americano;
            }
            else if (CoffeBean == 2 && Ice == 1)
            {
                return enumWorkResult.IceAmericano;
            }
            else if (Powder == 1 && Ice == 2)
            {
                return enumWorkResult.IceTea;
            }
            else if (CoffeBean == 1 && Milk == 2)
            {
                return enumWorkResult.CafeLatte;
            }
            else if (CoffeBean == 1 && Milk == 1 && Ice == 1)
            {
                return enumWorkResult.IceCafeLatte;
            }
            else
            {
                return enumWorkResult.Failed;
            }
        }
        else
        {
            if (Powder == 1 && Ice < 1)
            {
                return enumWorkResult.Failed;
            }
            else
            {
                return enumWorkResult.Unknown;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        recipe = new List<int>();
        ResultObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (recipe.Count == 0)
        {
            ResultObject.gameObject.SetActive(false);
        }
        else if (recipe.Count > 0)
        {
            Result.text = "Result = ";
            Result.text += CurrentMenu.ToString();
            ResultObject.gameObject.SetActive(true);
        }
    }

    public void StationClear()
    {
        ResultObject.transform.SetParent(transform);
        ResultObject.transform.position = transform.position;
        ResultObject.gameObject.SetActive(false);
        recipe.Clear();
        Result.text = "Result = ";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ResultObject.gameObject.activeSelf)
        {
            ResultObject.gameObject.SetActive(true);
            ResultObject.gameObject.transform.SetParent(CanvasTrs);
            ResultObject.transform.position = Input.mousePosition; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                                   // ItemDetach.gameObject.SetActive(true);        
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ResultObject.gameObject.activeSelf)
        {
            ResultObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ResultObject.gameObject.activeSelf)
        {
            ResultObject.transform.SetParent(transform);
            ResultObject.transform.position = transform.position;
        }
    }
}
