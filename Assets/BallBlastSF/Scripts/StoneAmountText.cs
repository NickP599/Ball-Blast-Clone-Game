using System;
using UnityEngine;
using TMPro;

public class StoneAmountText : MonoBehaviour
{
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private TMP_Text amountText;



    private void Awake()
    {
        amountText.text = spawner.SpawnLeft.ToString();

        spawner.OnSpawned.AddListener(ChangeAmountText);
    }

    private void OnDestroy()
    {
        spawner.OnSpawned.RemoveListener(ChangeAmountText);
    }

    public void ChangeAmountText()
    {
       

        amountText.text = (spawner.SpawnLeft).ToString();
    }
}
