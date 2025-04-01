using UnityEngine;
using TMPro;


[RequireComponent(typeof(Destructible))]
public class StoneHitPointsText : MonoBehaviour
{
    [SerializeField] private TMP_Text hitPointsText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.ChangeHitPoints.AddListener(OnChangeHitPoints);

       
    }

    private void OnDestroy()
    {
        destructible.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hitPoints = destructible.GetHitPoints();

        if (hitPoints >= 1000)
        {
            hitPointsText.text =  hitPoints / 1000 + "K";

        }
        else
        {
            hitPointsText.text = hitPoints.ToString();
        }


    }
}
