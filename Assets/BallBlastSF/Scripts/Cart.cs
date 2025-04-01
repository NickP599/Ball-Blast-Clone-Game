using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float vehicleWidh;
    [Header("Wheels")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelRadius;

    [HideInInspector] public UnityEvent OnStoneCollision;

    private float lastPositionX;
    private float deltaMovement;
    
    private Vector3 movementTarget;

    private void Start()
    {
        movementTarget = transform.position;
    }


    private void Update()
    {
        Move();

        RortateWheel();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         Stone stone = collision.transform.root.GetComponent<Stone>();

        if(stone != null)
        {
            OnStoneCollision.Invoke();
        }
    }

    private void Move()
    {
        lastPositionX = transform.position.x;
        
        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);

        deltaMovement = transform.position.x - lastPositionX;
    }

    private void RortateWheel()
    {
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheels.Length; i++)
        {
           wheels[i].Rotate(0,0,- angle);
        }
    }

    public void SetMovementTarget(Vector3 target)
    {
        movementTarget = ClampMovementTarget(target);
    }

    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBoard = LevelBoundary.Instance.LeftBorder + vehicleWidh * 0.5f;
        float righrBoard = LevelBoundary.Instance.RightBorder - vehicleWidh * 0.5f;

        Vector3 movTarget = target;

        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBoard) movTarget.x = leftBoard;
        if(movTarget.x > righrBoard) movTarget.x = righrBoard;

        return movTarget;
    }

#if UNITY_EDITOR  

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawLine(transform.position - new Vector3(vehicleWidh * 0.5f, 0.5f, 0), transform.position + new Vector3(vehicleWidh * 0.5f, - 0.5f, 0));
        
    }
#endif
}
