using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEntryPoint : MonoBehaviour
{
    public static FindEntryPoint instance;

    public Entry[] entries;
    public Entry noEntry;

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
}
