using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField] Image LeftPoint;
    [SerializeField] Image RightPoint;

    public Image LeftImage { get { return LeftPoint; } set { LeftPoint = value; } }
    public Image RightImage { get { return LeftPoint; } set { LeftPoint = value; } }

}
