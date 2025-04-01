using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField]private int coinAmount;

    public int CoinsAmount => coinAmount;


    [HideInInspector] public UnityEvent OnItemBag;

    public void CoinAdd(int coins)
    {
        if (coins < 0) return;

        coinAmount += coins;

        OnItemBag.Invoke();
    }

    public bool RemoveCoins(int coins)
    {
        if(coinAmount >= coins)
        {
            coinAmount -= coins;         

            OnItemBag.Invoke();

            return true;
        }

        return false;
    }

    public void SetCoinsFromPlayerPrefs(int coins)
    {
        coinAmount = coins;
    }
}
