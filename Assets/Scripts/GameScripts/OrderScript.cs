using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderScript : MonoBehaviour
{
    Button btnOrder;

    // Start is called before the first frame update
    void Start()
    {
        btnOrder = GetComponent<Button>();
        btnOrder.onClick.AddListener(() => Human.instance.StateSwithcing(PlayerState.ORDER));
    }
}
