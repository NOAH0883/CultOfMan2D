using Unity.VisualScripting;
using UnityEngine;
enum EnemyStates { idle, run, dead }
public class PassiveEnemy : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    Rigidbody2D rb;
    Transform playerPos;

    bool canSeePlayer;
    Vector2 runDir;
    [SerializeField] float health;
    
    EnemyStates enemyStates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyStates = EnemyStates.idle;
    }

    // Update is called once per frame
    void Update()
    {
        stateManager();

        switch (enemyStates)
        { 
            case EnemyStates.idle:
                Idle();
                break;

            case EnemyStates.run:
                Run();
                break;

            case EnemyStates.dead:

                break;
        }
    }

    void stateManager()
    {
        if (canSeePlayer)
            enemyStates = EnemyStates.run;
        else
            enemyStates = EnemyStates.idle;
    }

    void Idle()
    {
        //play idle animations
        Debug.Log("idle");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPos = collision.transform;
            
            canSeePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSeePlayer = false;
        }
    }

    void Run()
    {
        runDir = transform.position - playerPos.position;
        //play run animations
    }

    private void FixedUpdate()
    {
        if(canSeePlayer)
        {
            rb.MovePosition(rb.position + runDir * MoveSpeed * Time.deltaTime);
        }
        
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            enemyStates = EnemyStates.dead;
        }
    }

}
