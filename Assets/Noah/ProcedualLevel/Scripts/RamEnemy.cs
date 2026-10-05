using UnityEngine;
using UnityEngine.AI;

public class RamEnemy : MonoBehaviour
{

    enum EnemyStates { idle, attack, dead }
    EnemyStates enemyStates;

    NavMeshAgent agent;
    SpriteRenderer sr;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float enemySight;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        stateManager();

        switch (enemyStates)
        {
            case EnemyStates.idle:
                
                break;

            case EnemyStates.attack:
                
                break;

            case EnemyStates.dead:

                break;
        }
    }

    void stateManager()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemySight, playerLayer);
        if (hit != null)
        {
            //can see player
            sr.color = Color.red;
        }
        else
        {
            //cant see player
            sr.color = Color.white;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySight);
    }
}
