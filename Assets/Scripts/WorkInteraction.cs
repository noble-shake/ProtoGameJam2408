using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkInteraction : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] WorkObject work;

    public void OnBeginDrag(PointerEventData eventData)
    {
        work.DragTrs.SetParent(null);
        work.DragTrs.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ItemDetach.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        work.DragTrs.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        work.DragTrs.SetParent(work.transform);
        work.DragTrs.position = work.transform.position;
    }
}
