using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkObject : MonoBehaviour
{
    [SerializeField] GameObject FrontObject;

    public Transform DragTrs { get { return FrontObject.transform; } }
}
