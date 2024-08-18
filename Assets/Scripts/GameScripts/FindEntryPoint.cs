using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class FindEntryPoint : MonoBehaviour
{
    private static System.Random rng = new System.Random();

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

    public int AllocateTable()
    { 
        int count = entries.Length;

        List<int> entryIndexer = Enumerable.Range(0, count).ToList();

        var randomized = entryIndexer.OrderBy(x => rng.Next());
        foreach (var ridx in randomized)
        {
            if (entries[ridx].emptryCheck == false)
            {
                entries[ridx].emptryCheck = true;
                return ridx;
            }
        }
        return -1;
    }

    public void ReturnTable(int _entry)
    {
        entries[_entry].emptryCheck = false;
    }
}
