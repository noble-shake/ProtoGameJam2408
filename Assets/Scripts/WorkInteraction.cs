using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkInteraction : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] int WorkID;
    // [SerializeField] Image FrontImage;
    [SerializeField] SpriteRenderer FrontImage;
    [SerializeField] Transform CanvasTrs;

    public int workID { get { return WorkID; } }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData == null) return;

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        FrontImage.gameObject.transform.SetParent(null);
        FrontImage.gameObject.transform.SetAsLastSibling();

        // FrontImage.transform.position = Input.mousePosition; // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FrontImage.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // ItemDetach.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
        {
            return;
        }

        // FrontImage.transform.position = Input.mousePosition;
        FrontImage.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        FrontImage.transform.SetParent(transform);
        FrontImage.transform.position = transform.position;
    }
}
