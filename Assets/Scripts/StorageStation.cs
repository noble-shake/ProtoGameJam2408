using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StorageStation : MonoBehaviour, IDropHandler
{
    public int Americano;
    public int IceAmericano;
    public int CafeLatte;
    public int IceCafeLatte;
    public int IceTea;


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData == null) return;

        if (eventData.pointerDrag.CompareTag("WorkStation"))
        { 
            WorkStation wo = eventData.pointerDrag.GetComponent<WorkStation>();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Americano = 0;
        IceAmericano = 0;
        CafeLatte = 0;
        IceCafeLatte = 0;
        IceTea = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
