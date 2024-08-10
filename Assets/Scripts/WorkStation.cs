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
            }

            ResultObject.gameObject.SetActive(true);
        }
    }

    private enumWorkResult MixRecipe(enumBasicWork target)
    {
        if (recipe.Count == 0)
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

        return enumWorkResult.Unknown;
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
            foreach (int idx in recipe)
            {
                Result.text += $"_{idx}_";
            }
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
