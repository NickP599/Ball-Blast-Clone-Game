using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private float startSpawnOffsetZ;

    [SerializeField] private LevelProgress levelProgress;


    [Header("Balance")]
    [SerializeField] private Turret turret;
    [SerializeField] private int spawnAmount;
    [SerializeField][Range(0.0f, 1.0f)] private float minHitPointsPercentage;
    [SerializeField] private float maxHitPointsRate;

    [HideInInspector] public UnityEvent Completed;
    [HideInInspector] public UnityEvent OnSpawned;

  

   public int SpawnLeft
    {
        get { return (spawnAmount - 1) - spawnedAmount; }
    }

    private int stoneMaxHitPoints;
    private int stoneMinHitPoints;
    private float timer;
    private int spawnedAmount;

   

    private void Start()
    {
        spawnAmount += levelProgress.currentLevel;

        int damagePerSecond = (int)((turret.Damage * turret.ProjectileAmount) * (1 / turret.FireRate));
        stoneMaxHitPoints = (int)(damagePerSecond * maxHitPointsRate);
        stoneMinHitPoints = (int)(stoneMaxHitPoints * minHitPointsPercentage);
        timer = spawnRate;

    }

    private void Update()
    {
      
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            Spawn();
            timer = 0;
        }
        if (spawnedAmount == spawnAmount)
        {
            enabled = false;
            Completed.Invoke();
        }

    }

    private void Spawn()
    {
        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        spawnPos.z += spawnedAmount * startSpawnOffsetZ;

        Stone stone = Instantiate(stonePrefab, spawnPos, Quaternion.identity);
        stone.SetSize((StoneSize)Random.Range(1, 4));
        stone.SetRandomColor(stone.spriteRenderer);

        stone.maxHitPoints = Random.Range(stoneMinHitPoints, stoneMaxHitPoints + 1);

        OnSpawned.Invoke();

        spawnedAmount++;

    }
}
