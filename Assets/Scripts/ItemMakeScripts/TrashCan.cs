using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    Juice juiceObject;

    private void Update()
    {
        if (juiceObject != null)
        {
            CookingStation.instance.StationClear();
        }
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
            juiceObject = null;
        }
    }
}
