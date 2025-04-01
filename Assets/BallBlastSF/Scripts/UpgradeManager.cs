using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private Bag bag;

    [Header("Text")]
    [SerializeField] private TMP_Text[] damageCostText;
    [SerializeField] private TMP_Text[] FireRateCostText;
    [SerializeField] private TMP_Text[] projectileAmountCostText;

    [Header("Cost")]
    [SerializeField] private int damageCost;
    [SerializeField] private int fireRateCost;
    [SerializeField] private int projectileAmountCost;

    [Header("Balance")]
    [SerializeField] private int addDamage;
    [SerializeField] private float RemoveFireRate;
    [SerializeField] private int AddprojectileAmount;

    private void Start()
    {
        for (int i = 0; i < damageCostText.Length; i++)
        {
            damageCostText[i].text = damageCost.ToString();
            FireRateCostText[i].text = fireRateCost.ToString();
            projectileAmountCostText[i].text = projectileAmountCost.ToString();
        }


    }

    public void AddDamage()
    {
        if (bag.RemoveCoins(damageCost) == true)
        {
            turret.AddDamage(addDamage);
        }
    }

    public void AddFireRate()
    {
        if (turret.FireRate == 0.2f) return;

        if (bag.RemoveCoins(fireRateCost) == true)
        {
            turret.RemoveFireRate(RemoveFireRate);
        }
    }

    public void AddProjectile()
    {
        if (turret.ProjectileAmount == 5) return;

        if (bag.RemoveCoins(projectileAmountCost) == true)
        {
            turret.AddProjectile(AddprojectileAmount);
        }
    }
}
