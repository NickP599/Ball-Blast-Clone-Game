using UnityEngine;
using TMPro;
using System;

public class CoinAmountText : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Bag bag;

    private void Awake()
    {
        bag.OnItemBag.AddListener(OnChangeCoinsAmount);
    }

    private void OnDestroy()
    {
        bag.OnItemBag.RemoveListener(OnChangeCoinsAmount);
    }

    public void OnChangeCoinsAmount()
    {
        int coinsAmount = bag.CoinsAmount;

        coinText.text = coinsAmount.ToString();
    }
}
