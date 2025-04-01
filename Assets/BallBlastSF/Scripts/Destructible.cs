using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public int maxHitPoints;

    public AudioSource OnDestroyedSound;

  [HideInInspector] public UnityEvent Die;
  [HideInInspector] public UnityEvent ChangeHitPoints;

    private int hitPoints;
    private bool isDie = false;

    private void Start()
    {
        hitPoints = maxHitPoints;
        ChangeHitPoints.Invoke();
    }

    public void ApplyDamage(int damage)
    {     

        hitPoints -= damage;
        ChangeHitPoints.Invoke();

        if (hitPoints <= 0)
        {
            Kill();
        }
    }

    public void AddHitPoints(int hitPoints)
    {
        if (hitPoints < 0) return;

        this.hitPoints += hitPoints;

        if (this.hitPoints > maxHitPoints)
            this.hitPoints = maxHitPoints;

        ChangeHitPoints.Invoke();
    }

    public void Kill()
    {
        if (isDie) return;

        hitPoints = 0;

        Die.Invoke();
        ChangeHitPoints.Invoke();

        isDie = true;

        if (OnDestroyedSound != null)
        {
            OnDestroyedSound.Play();
        }
        else
        {
            Debug.LogWarning(" Warning: The destroyed object's sound reference is null. Please ensure a valid audio clip is assigned.");
        }
    }

    public int GetHitPoints()
    {
        return hitPoints;
    }

}