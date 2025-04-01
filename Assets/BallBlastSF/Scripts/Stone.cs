using System;
using UnityEngine;
using UnityEngine.Events;

public enum StoneSize
{
    Small,
    Medium,
    Big,
    Huge
}

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    [SerializeField] private StoneSize size;
    [SerializeField] private float spawnUpForce;
    [SerializeField] private float offSetZ;
    [SerializeField] private ImpactEffect impactEffectPrefab;

    [Header("StoneColors")]
    public Color[] StoneColors;

    private StoneMovement stoneMovement;
    [HideInInspector]public SpriteRenderer spriteRenderer;


    private void Awake()
    {
        stoneMovement = GetComponent<StoneMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Die.AddListener(OnStoneDestroyed);

        SetSize(size);
    }

  
    private void OnDestroy()
    {
        Die.RemoveListener(OnStoneDestroyed);

    }

    private void OnStoneDestroyed()
    {
        SpawnCoin();

        if (size != StoneSize.Small)
        {
            SpawnStones();
        }

        Destroy(gameObject);
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {
            float OffSet = offSetZ;

            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + OffSet);
            stone.SetSize(size - 1);
            stone.SetRandomColor(spriteRenderer);
            stone.maxHitPoints = Math.Clamp(maxHitPoints / 2, 1, maxHitPoints);
            stone.stoneMovement.AddVerticalvelocity(spawnUpForce);
            stone.stoneMovement.SetHorizontalDerection((i % 2 * 2) - 1);

            offSetZ += OffSet;
        }
      
    }


    public void SetSize(StoneSize size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    public void SetRandomColor(SpriteRenderer spriteRenderer)
    { 
        Color color = StoneColors[UnityEngine.Random.Range(0, StoneColors.Length)];
        color.a = 1;

        spriteRenderer.color = color;

    }

    private Vector3 GetVectorFromSize(StoneSize size)
    {
        if (size == StoneSize.Huge) return new Vector3(1, 1, 1);
        if (size == StoneSize.Big) return new Vector3(0.75f, 0.75f, 0.75f);
        if (size == StoneSize.Medium) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == StoneSize.Small) return new Vector3(0.4f, 0.4f, 0.4f);

        return Vector3.one;
    }

    private void SpawnCoin()
    {
        int random = UnityEngine.Random.Range(1, 11);

        if(random % 2 == 0) return;

        if (impactEffectPrefab != null)
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
    }
}
