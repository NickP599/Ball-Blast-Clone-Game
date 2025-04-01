using UnityEngine;

public class Coin : PickUp
{
   

    [SerializeField] private ImpactEffect ImpactEffectPrefab;

    [SerializeField] private float gravity;

    private bool tryToEnableGravity;
    private void Start()
    {
        tryToEnableGravity = true;
    }
    private void Update()
    {
        if (tryToEnableGravity)
            transform.position = new Vector3(transform.position.x, transform.position.y - gravity * Time.deltaTime, transform.position.z);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Cart cart = collision.transform.root.GetComponent<Cart>();
        Bag bag = collision.transform.root.GetComponent<Bag>();
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (cart != null)
           Instantiate(ImpactEffectPrefab);

        if (bag != null)
            bag.CoinAdd(1);

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                tryToEnableGravity = false;

            }
        }
    }


}
