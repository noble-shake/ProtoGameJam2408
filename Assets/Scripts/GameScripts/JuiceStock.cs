using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceStock : MonoBehaviour
{
    Image image;
    [SerializeField] Image fillImage;

    public float stock { get { return fillImage.fillAmount; } set { fillImage.fillAmount = value; } }

    public void Start()
    {
        image = GetComponent<Image>();
    }
}
