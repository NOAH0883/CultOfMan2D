using UnityEngine;

public class Boss1_Active : MonoBehaviour
{
    public float actionInterval;
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    [SerializeField] private float chargeAttackRange;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void Boss1_ActiveUpdate()
    {
        if (actionInterval > 0)
        {
            Vector3 direction = Vector3.zero;
            direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            actionInterval -= Time.deltaTime;
        }

        if (actionInterval <= 0)
        {
            float distCheck = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log("Player pos: " + player.transform.position);
            Debug.Log("Boss pos: " + transform.position);
            Debug.Log("Distance: " + distCheck);
            if (Vector3.Distance(transform.position, player.transform.position) > chargeAttackRange)
            {
                moveSpeed = sprintSpeed;
                actionInterval = 1;
            }
            else
            {
                moveSpeed = walkSpeed;
                actionInterval = 3;
                Debug.Log("Taking action.");
            }
                
        }
    }
}
