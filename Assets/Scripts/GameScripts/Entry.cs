using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    [SerializeField] public Image AllocatedTable;
    bool empty = false;
    [SerializeField] List<GameObject> Paths;

    public bool emptryCheck { get { return empty; } set { empty = value; } }

    public List<GameObject> Path { get { return Paths; } }

}
