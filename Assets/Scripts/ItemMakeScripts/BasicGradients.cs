using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGradients : MonoBehaviour
{
    [SerializeField] public enumBasicWork gradient;
    [SerializeField] SpriteRenderer FrontImage;
    [SerializeField] SpriteRenderer BackgroundImage;
    [SerializeField] bool mouseClick;

    BoxCollider2D FrontImageColl;
    // [SerializeField] SpriteRenderer Shadow;


    private void Start()
    {
        FrontImageColl = FrontImage.GetComponent<BoxCollider2D>();
        // FrontImage.GetComponent<Gradients>().GradientType = gradient;
    }


    private void OnMouseDown()
    {
        mouseClick = true;
        FrontImage.transform.SetParent(null);
        FrontImage.transform.SetAsLastSibling();

        // FrontImage.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MouseDragCheck()
    {


        if (Input.GetMouseButtonUp(0))
        {
            mouseClick = false;
            FrontImage.transform.SetParent(transform);
            FrontImage.transform.SetAsLastSibling();
            FrontImage.transform.position = transform.position;
            FrontImageColl.enabled = false;
        }

        if (!mouseClick) return;

        // Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FrontImage.gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FrontImageColl.enabled = true;
    }

    private void LateUpdate()
    {
        MouseDragCheck();
    }

    private void Update()
    {
        
    }

    //private void OnMouseDrag()
    //{
    //    FrontImage.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}

    //private void OnMouseExit()
    //{

    //    FrontImage.transform.SetParent(transform);
    //    FrontImage.transform.SetAsLastSibling();
    //    FrontImage.transform.position = transform.position;
    //}
}
