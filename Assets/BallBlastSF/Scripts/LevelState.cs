using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private Cart cart;
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] LevelProgress levelProgress;
   

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private Stone[] stones;

   private float timer;
   private bool cheeckPassed;

    private void Awake()
    {
        cart.OnStoneCollision.AddListener(OnCartCollisionStone);
        spawner.Completed.AddListener(OnSpawnCompleted);
    }

    private void Update()
    {
        stones = FindObjectsByType<Stone>(FindObjectsSortMode.None);

        timer += Time.deltaTime;

       if(timer > 0.5f)
        {
            if (cheeckPassed)
            {
                if(stones.Length == 0)
                {
                    Passed.Invoke();
                    levelProgress.currentLevel++;
                }
            }

            timer = 0;
            Debug.Log("Количество какмней на сцене: " + stones.Length);
        }
    }

    private void OnDestroy()
    {
        cart.OnStoneCollision.RemoveListener(OnCartCollisionStone);
        spawner.Completed.RemoveListener(OnSpawnCompleted);
    }

    private void OnCartCollisionStone()
    {
       Defeat.Invoke();
    }

    private void OnSpawnCompleted()
    {
        cheeckPassed = true;
    }

}
