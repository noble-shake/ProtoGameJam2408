using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    Toggle RecipeParent;
    public Button BackButton;

    public void SetToggleParent(Toggle _parent)
    { 
        RecipeParent = _parent;
    }

    private void OnDisable()
    {
        RecipeParent.isOn = false;
    }

    private void OnDestroy()
    {
        RecipeParent.isOn = false;
    }

    private void Start()
    {
        BackButton = GetComponent<Button>();
        BackButton.onClick.AddListener(OnClickTrigger);
    }

    public void OnClickTrigger()
    {
        Debug.Log("execute Destroy"); 
        RecipeParent.isOn = false; Destroy(gameObject);
    }
}
