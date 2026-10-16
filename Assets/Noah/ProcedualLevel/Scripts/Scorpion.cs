using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.AI;
using static IDamageable;


public class Scorpion : MonoBehaviour, Damageable
{

    enum EnemyStates { idle, attack, run ,dead }
    EnemyStates enemyStates;

    NavMeshAgent agent;
    SpriteRenderer sr;
    Rigidbody2D rb;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float enemySight;

    [SerializeField] float movementRange;

    bool isMoving;
    Vector2 movePos;

    bool isAttacking;
    Vector2 playerPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;


    [SerializeField] float enemyHealth;
    [SerializeField] int foodAmount;


    float moveSpeed;
    Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        moveSpeed = agent.speed;
    }

    void Update()
    {
        stateManager();

        switch (enemyStates)
        {
            case EnemyStates.idle:
                agent.isStopped = false;

                if (!isMoving)
                {
                    Vector3 point;
                    if (RandomPoint(transform.position, movementRange, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                        movePos = point;

                        StartCoroutine(Idle());
                    }
                }


                break;

            case EnemyStates.attack:

                

                if (!isMoving)
                {
                    Vector3 point;
                    if (RandomPoint(transform.position, movementRange/2, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                        movePos = point;

                        StartCoroutine(Idle());
                    }
                }

                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
                break;
            
            case EnemyStates.run:
                
                
                break;

            case EnemyStates.dead:
                dead();
                break;
        }
    }


    void stateManager()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemySight, playerLayer);
        if (hit != null)
        {
            enemyStates = EnemyStates.attack;
            playerPos = hit.transform.position;
        }
        else
        {
            enemyStates = EnemyStates.idle;
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;

        return false;
    }

    IEnumerator Idle()
    {
        isMoving = true;

        agent.destination = movePos;

        while (agent.remainingDistance > 1)
        {
            animator.SetBool("IsWalking", true);
            agent.destination = movePos;
            yield return null;
        }
        animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(5);

        isMoving = false;
    }

    IEnumerator Attack()
    {
        
        agent.isStopped = true;
        agent.ResetPath();
        isAttacking = true;
        animator.SetBool("IsWalking", false);

        sr.color = Color.red;
        yield return new WaitForSeconds(1);

        Vector2 attackDir = playerPos - rb.position;
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,angle));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(attackDir * bulletSpeed, ForceMode2D.Impulse);

        rb.linearVelocity = Vector3.zero;
        sr.color = Color.white;

        yield return new WaitForSeconds(1);


        //move
        agent.isStopped = false;
        isMoving = true;
        float normalSpeed = agent.speed;
        agent.speed *= 4;


        agent.destination = movePos;

        while (agent.remainingDistance > 1)
        {
            animator.SetBool("IsWalking", true);
            agent.destination = movePos;
            yield return null;
        }

        animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(0.5f);

        agent.speed = normalSpeed;
        isMoving = false;

        isAttacking = false;

    }


    public void Damage(float damage, Vector2 hitPos, float knockBackPower)
    {
        SoundManager.PlaySound(SoundType.hit, 0.4f);
        StartCoroutine(KnockBack(knockBackPower, hitPos));

        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            SoundManager.PlaySound(SoundType.enemyDead, 0.5f);
            GameData.food += foodAmount;
            Destroy(gameObject);
        }
    }

    IEnumerator KnockBack(float knockBackPower, Vector2 hitPos)
    {

        Debug.Log("--knockBackEnemy--");
        agent.isStopped = true;
        Vector2 KnockBackDir = (rb.position - hitPos).normalized;
        rb.linearVelocity = Vector3.zero;

        rb.AddForce(KnockBackDir * knockBackPower, ForceMode2D.Impulse);



        yield return new WaitForSeconds(0.25f);
        rb.linearVelocity = Vector3.zero;
        agent.isStopped = false;
    }

    void dead()
    {
        //play death animation  - would be a corutine
        //give the player food about 

        //destory game object

    }

}
