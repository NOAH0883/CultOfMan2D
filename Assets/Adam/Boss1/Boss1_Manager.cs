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
            default: break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = BossState.ACTIVE;
            Debug.Log("Boss is active.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = BossState.IDLE;
            Debug.Log("Boss is idle.");
        }
    }
}
