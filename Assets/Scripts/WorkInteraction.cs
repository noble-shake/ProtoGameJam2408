using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkInteraction : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] int WorkID;
    [SerializeField] Image FrontImage;
    [SerializeField] Transform CanvasTrs;

    public int workID { get { return WorkID; } }

    public void OnBeginDrag(PointerEventData eventData)
    {
        FrontImage.gameObject.transform.SetParent(CanvasTrs);
        FrontImage.transform.position = Input.mousePosition; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ItemDetach.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        FrontImage.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        FrontImage.transform.SetParent(transform);
        FrontImage.transform.position = transform.position;
    }
}
