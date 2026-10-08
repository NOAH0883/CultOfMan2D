using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static IDamageable;


public class RamEnemy : MonoBehaviour, Damageable
{

    enum EnemyStates { idle, attack, dead }
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
    [SerializeField] Vector2 attackHitBox;
    [SerializeField] float attackForce;
    [SerializeField] float enemyDamage;
    [SerializeField] float knockBackPower;


    [SerializeField] float enemyHealth;
    [SerializeField] int foodAmount;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();    
        rb = GetComponent<Rigidbody2D>();
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

                agent.isStopped = true;

                if(!isAttacking)
                {
                    StartCoroutine(Attack());
                }
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
            agent.destination = movePos;
            yield return null;  
        }

        yield return new WaitForSeconds(5);

        isMoving = false;
    }

    IEnumerator Attack()
    {
        agent.isStopped = true;
        agent.ResetPath();
        isAttacking =  true;

        rb.linearVelocity = Vector3.zero;
        sr.color = Color.red;
        yield return new WaitForSeconds(1);
       

        Vector2 attackDir = playerPos - rb.position;
        rb.AddForce(attackDir * attackForce, ForceMode2D.Impulse);

        float attackDuration = .5f;
        float timer = 0f;
        bool hasDamagedPlayer = false;

        while (timer < attackDuration)
        {
            timer += Time.deltaTime;

            RaycastHit2D hit = Physics2D.BoxCast(transform.position, attackHitBox, 0f, Vector2.zero, 0f,playerLayer);
            if (hit && !hasDamagedPlayer)
            {
                if (hit.collider.TryGetComponent<Damageable>(out Damageable damageableObject))
                {

                    rb.linearVelocity = Vector3.zero;
                    Vector2 pos = rb.position;
                    damageableObject.Damage(enemyDamage, pos, knockBackPower);
                    hasDamagedPlayer = true;
                }
            }
            yield return null;
        }

  
        rb.linearVelocity = Vector3.zero;
        sr.color = Color.white;

        yield return new WaitForSeconds(1);

        isAttacking = false;


    }


    public void Damage(float damage, Vector2 hitPos, float knockBackPower)
    {
        StartCoroutine(KnockBack(knockBackPower, hitPos));

        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            GameData.food += foodAmount;
            Destroy(gameObject);
        }
            

    }

    IEnumerator KnockBack(float knockBackPower, Vector2 hitPos)
    {

       Debug.Log("--knockBackEnemy--");
       Vector2 KnockBackDir = (rb.position - hitPos).normalized;
        rb.linearVelocity = Vector3.zero;

        rb.AddForce(KnockBackDir * knockBackPower, ForceMode2D.Impulse);
        


        yield return new WaitForSeconds(0.25f);
        rb.linearVelocity = Vector3.zero;

    }

    void dead()
    {
        //play death animation  - would be a corutine
        //give the player food about 

        //destory game object

      

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySight);

        Gizmos.DrawWireCube(transform.position, attackHitBox);
    }
}
