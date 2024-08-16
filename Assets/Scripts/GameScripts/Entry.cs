using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    bool empty;
    [SerializeField] List<GameObject> Paths;

    public bool emptryCheck { get { return empty; } set { empty = value; } }

    public List<GameObject> Path { get { return Paths; } }

}
