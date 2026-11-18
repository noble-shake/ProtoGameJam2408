using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    Toggle DisplayParent;
    public Button BackButton;

    [SerializeField] Toggle Buy1;
    [SerializeField] Toggle Buy2;
    [SerializeField] Toggle Buy3;
    [SerializeField] Toggle StoreUp;

    [SerializeField]TMP_Text Buy1Remain;
    [SerializeField]TMP_Text Buy2Remain;
    [SerializeField]TMP_Text Buy3Remain;
    [SerializeField]TMP_Text StoreUpCheck;
    [SerializeField]TMP_Text StoreUpPrice;

    public void SetToggleParent(Toggle _parent)
    {
        DisplayParent = _parent;
    }

    private void OnDisable()
    {
        DisplayParent.isOn = false;
    }

    private void OnDestroy()
    {
        DisplayParent.isOn = false;
    }

    private void Start()
    {
        BackButton = GetComponent<Button>();
        BackButton.onClick.AddListener(OnClickDisplay);
        Buy1.onValueChanged.AddListener((eventData) => { OnClickBuy(enumStore.AirCond1, eventData); });
        Buy2.onValueChanged.AddListener((eventData) => { OnClickBuy(enumStore.AirCond2, eventData); });
        Buy3.onValueChanged.AddListener((eventData) => { OnClickBuy(enumStore.Fan, eventData); });
        StoreUp.onValueChanged.AddListener((eventData) => { OnClickBuy(enumStore.StoreUp, eventData); });

    }

    public void OnClickDisplay()
    {
        DisplayParent.isOn = false;
        Destroy(gameObject);
    }

    public void OnClickBuy(enumStore _store, bool isOn)
    {
        if (isOn)
        {
            GameManager.instance.BuyObject(_store);
        }
    }


    private void FixedUpdate()
    {
        float buy1remain = GameManager.instance.buy1Remain;
        float buy2remain = GameManager.instance.buy2Remain;
        float buy3remain = GameManager.instance.buy3Remain;

        BuyEffect(buy1remain, Buy1Remain, Buy1);
        BuyEffect(buy2remain, Buy2Remain, Buy2);
        BuyEffect(buy3remain, Buy3Remain, Buy3);

        if (GameManager.instance.StoreUpgrade == true)
        {
            StoreUp.interactable = false;
            StoreUpPrice.gameObject.SetActive(false);
            StoreUpCheck.text = "Done";
        }
        else
        {
            StoreUp.interactable = true;
            StoreUpCheck.text = "Upgrade";
        }
    }

    private void BuyEffect(float _remain, TMP_Text _disp, Toggle _icon)
    {
        if (_remain > 0f)
        {
            _disp.text = TimeSpan.FromSeconds(_remain).ToString(@"mm\:ss");
            _icon.interactable = false;
        }

        if (_remain <= 0f)
        {
            _disp.text = "buy (3min)";
            _icon.interactable = true;
            _icon.isOn = false;
        }
    }
}
