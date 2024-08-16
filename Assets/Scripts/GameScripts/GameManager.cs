using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;





public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    // Check Order State
    // Check Guest In Table.
    // Check Entry Points

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

    private void Update()
    {
        
    }

    private bool OrderCheck()
    {
        // GuestManager.instance


        return false;
    }
}
