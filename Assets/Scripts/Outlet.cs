using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Outlet : MonoBehaviour, IDropHandler
{
    [SerializeField] WorkStation work;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.CompareTag("WorkStation"))
        {
            work.StationClear();
        }
    }
}
