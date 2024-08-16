using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{ 
    ORDER,
    COOK,
}

public class Human : MonoBehaviour
{
    public static Human instance;
    [SerializeField] PlayerState CurrentState;
    SpriteRenderer CurrentImage;
    [SerializeField] Sprite OrderImage;
    [SerializeField] Sprite CookImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(instance);
        }
    }

    private void Start()
    {
        CurrentImage = GetComponent<SpriteRenderer>();
        CurrentImage.sprite = OrderImage;
        CurrentState = PlayerState.ORDER;
    }

    private void Update()
    {
        Behaviour();
    }

    public void StateSwithcing(PlayerState _state)
    {
        CurrentState = _state;
    }

    private void Behaviour()
    {
        switch (CurrentState)
        { 
            case PlayerState.ORDER:
                CurrentImage.sprite = OrderImage;
                break;

            case PlayerState.COOK:
                CurrentImage.sprite = CookImage;
                break;
        }
    }
}
