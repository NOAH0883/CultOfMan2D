using Unity.Cinemachine;
using UnityEngine;

public class Boss1_Manager : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    private CircleCollider2D circleCollider;
    [SerializeField] float aggroRadius;
    enum BossState
    {
        IDLE, ACTIVE, DEAD
    }
    private BossState state;

    [SerializeField] private Boss1_Idle Boss1_Idle;
    [SerializeField] private Boss1_Active Boss1_Active;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = aggroRadius;
        state = BossState.IDLE;
        Boss1_Idle.walkSpeed = walkSpeed;
        Boss1_Active.walkSpeed = walkSpeed;
        if (sprintSpeed > walkSpeed)
        {
            Boss1_Active.sprintSpeed = sprintSpeed;
        }
        else
        {
            Boss1_Active.sprintSpeed = walkSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BossState.IDLE: Boss1_Idle.Boss1_IdleUpdate(); break;
            case BossState.ACTIVE: Boss1_Active.Boss1_ActiveUpdate(); break;
            default: break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = BossState.ACTIVE;
            Boss1_Active.moveSpeed = walkSpeed;
            Boss1_Active.actionInterval = 3;
            circleCollider.radius = aggroRadius * 4;
            Debug.Log("Boss is active.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = BossState.IDLE;
            Boss1_Idle.walkSpeed = walkSpeed;
            Boss1_Idle.idleTime = 0;
            Boss1_Idle.CompareDistance();
            circleCollider.radius = aggroRadius;
            Debug.Log("Boss is idle.");
        }
    }
}
