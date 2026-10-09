using Unity.Cinemachine;
using UnityEngine;

public class Boss1_Manager : MonoBehaviour
{

    private CircleCollider2D circleCollider;
    [SerializeField] float aggroRadius;
    enum BossState
    {
        IDLE, ACTIVE, DEAD
    }
    private BossState state;

    [SerializeField] private Boss1_Active Boss1_Active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = aggroRadius;
        state = BossState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BossState.IDLE: break;
            case BossState.ACTIVE: Boss1_Active.Boss1_ActiveUpdate(); break;
            default: break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = BossState.ACTIVE;
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
            circleCollider.radius = aggroRadius;
            Debug.Log("Boss is idle.");
        }
    }
}
