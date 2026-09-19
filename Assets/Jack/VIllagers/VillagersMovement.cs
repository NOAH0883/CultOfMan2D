using UnityEngine;

public class VillagerAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float minWalkTime = 2f;
    public float maxWalkTime = 5f;

    [Header("Idle Settings")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;

    [Header("Zone Configuration")]
    [Tooltip("Drag your CampZone object here from the Hierarchy!")]
    public Transform campZone;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float stateTimer;
    private float targetStateTime;
    private bool isWalking;

    public bool isSick;
    public int foodNeeded = 2;

    [SerializeField] Vector2 box;
    Vector2 boxPos;





    void Start()
    {

        boxPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        TransitionToIdle();
    }

    void Update()
    {
        stateTimer += Time.deltaTime;

        if (stateTimer >= targetStateTime)
        {
            if (isWalking)
            {
                TransitionToIdle();
            }
            else
            {
                TransitionToWalk();
            }
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
        {

            float leftLimit = boxPos.x - (box.x / 2f);
            float rightLimit = boxPos.x + (box.x / 2f);
            float bottomLimit = boxPos.y - (box.y / 2f);
            float topLimit = boxPos.y + (box.y / 2f);



            //float leftLimit = zoneCenter.x - (zoneSize.x / 2f);
            //float rightLimit = zoneCenter.x + (zoneSize.x / 2f);
            //float bottomLimit = zoneCenter.y - (zoneSize.y / 2f);
            //float topLimit = zoneCenter.y + (zoneSize.y / 2f);

            Vector2 currentPos = transform.position;

            // If he hits any wall, instantly flip his direction back inward
            if (currentPos.x <= leftLimit && moveDirection.x < 0) moveDirection.x *= -1;
            if (currentPos.x >= rightLimit && moveDirection.x > 0) moveDirection.x *= -1;
            if (currentPos.y <= bottomLimit && moveDirection.y < 0) moveDirection.y *= -1;
            if (currentPos.y >= topLimit && moveDirection.y > 0) moveDirection.y *= -1;

            moveDirection = moveDirection.normalized;

            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void TransitionToWalk()
    {
        isWalking = true;
        stateTimer = 0f;
        targetStateTime = Random.Range(minWalkTime, maxWalkTime);

        // Pick a completely random direction to start walking
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void TransitionToIdle()
    {
        isWalking = false;
        stateTimer = 0f;
        targetStateTime = Random.Range(minIdleTime, maxIdleTime);
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, box);
    }
}
