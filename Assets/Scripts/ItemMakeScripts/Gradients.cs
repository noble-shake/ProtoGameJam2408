using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradients : MonoBehaviour
{
    public enumBasicWork GradientType;

    private void Start()
    {
        GradientType = GetComponentInParent<BasicGradients>().gradient;
    }
}
