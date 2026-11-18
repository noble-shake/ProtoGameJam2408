using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CookingStation : MonoBehaviour
{
    public static CookingStation instance;

    bool mouseClick;
    enumWorkResult CurrentMenu;
    Gradients IncomeGradient;
    [SerializeField] Transform CanvasTrs;
    List<int> recipe; // 0 : Bean, 1 : Ice, 2 : Mixer, 3 : Milk

    BoxCollider2D JuiceCollider;

    [SerializeField] Juice juiceObject;

    private void OnMouseDown()
    {
        if (juiceObject.gameObject.activeSelf == false) return;

        mouseClick = true;
        juiceObject.transform.SetParent(null);
        juiceObject.transform.SetAsLastSibling();
    }

    private void MouseDragCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IncomeGradient != null)
            {
                if (recipe.Count < 3)
                {
                    if (juiceObject.gameObject.activeSelf == false)
                    {
                        juiceObject.gameObject.SetActive(true);
                    }
                    recipe.Add((int)IncomeGradient.GradientType);
                    CurrentMenu = MixRecipe((enumBasicWork)System.Enum.Parse(typeof(enumBasicWork), System.Enum.GetName(typeof(enumBasicWork), (int)IncomeGradient.GradientType)));
                    juiceObject.menuType = CurrentMenu;
                    Sprite item_image = GameManager.instance.getImage((dynamicSprites)System.Enum.Parse(typeof(dynamicSprites), CurrentMenu.ToString()));
                    juiceObject.setSprite(item_image);
                    // juiceObject.sprite = 
                    GameManager.instance.JuiceCounting();

                }

                juiceObject.gameObject.SetActive(true);
                Human.instance.StateSwithcing(PlayerState.COOK);

                IncomeGradient = null;
            }
            mouseClick = false;
            juiceObject.transform.SetParent(transform);
            juiceObject.transform.SetAsLastSibling();
            juiceObject.transform.position = transform.position;
            JuiceCollider.enabled = false;
        }

        if (!mouseClick) return;

        // Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        juiceObject.gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        JuiceCollider.enabled = true;
    }

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

    private void Start()
    {
        recipe = new List<int>();
        juiceObject.gameObject.SetActive(false);
        JuiceCollider = juiceObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        MouseDragCheck();

        if (recipe.Count == 0)
        {
            juiceObject.gameObject.SetActive(false);
        }
        else if (recipe.Count > 0)
        {
            juiceObject.gameObject.SetActive(true);
        }
    }

    public void StationClear()
    {
        juiceObject.transform.SetParent(transform);
        juiceObject.transform.position = transform.position;
        juiceObject.gameObject.SetActive(false);
        recipe.Clear();
    }

    private enumWorkResult MixRecipe(enumBasicWork target)
    {
        int CoffeBean = 0;
        int Ice = 0;
        int Powder = 0;
        int Milk = 0;

        if (recipe.Count == 1)
        {
            switch (target)
            {
                case enumBasicWork.coffeebean:
                    return enumWorkResult.coffeebean;
                case enumBasicWork.ice:
                    return enumWorkResult.ice;
                case enumBasicWork.poweder:
                    return enumWorkResult.poweder;
                case enumBasicWork.milk:
                    return enumWorkResult.milk;
            }
        }

        for (int idx = 0; idx < recipe.Count; idx++)
        {
            switch (recipe[idx])
            {
                case 0:
                    CoffeBean++;
                    break;
                case 1:
                    Ice++;
                    break;
                case 2:
                    Powder++;
                    break;
                case 3:
                    Milk++;
                    break;
            }
        }

        if (recipe.Count == 3)
        {
            if (CoffeBean == 3)
            {
                return enumWorkResult.hot_ame;
            }
            else if (CoffeBean == 2 && Ice == 1)
            {
                return enumWorkResult.ice_ame;
            }
            else if (Powder == 1 && Ice == 2)
            {
                return enumWorkResult.ice_tea;
            }
            else if (CoffeBean == 1 && Milk == 2)
            {
                return enumWorkResult.hot_latte;
            }
            else if (CoffeBean == 1 && Milk == 1 && Ice == 1)
            {
                return enumWorkResult.ice_latte;
            }
            else
            {
                return enumWorkResult.fail;
            }
        }
        else
        {
            if (Powder == 1 && Ice < 1)
            {
                return enumWorkResult.fail;
            }
            else
            {
                return enumWorkResult.unknown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Gradient"))
        {
            Debug.Log("Gradient Trigger");
            IncomeGradient = collision.GetComponent<Gradients>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Gradient"))
        {
            IncomeGradient = collision.GetComponent<Gradients>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Gradient"))
        {
            IncomeGradient = null;
        }
    }

}
