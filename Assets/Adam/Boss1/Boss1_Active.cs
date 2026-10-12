using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Boss1_Active : MonoBehaviour
{
    public float actionInterval;
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    [SerializeField] private float chargeSpeed;
    private Vector3 direction;
    
    [SerializeField] private float chargeAttackRange;
    [SerializeField] private float meleeAttackRange;
    public GameObject boss1HitboxVisuals;
    public SpriteRenderer b1HSprite;
    public GameObject boss1IndicatorVisuals;
    public SpriteRenderer b1ISprite;
    public GameObject boss1Hitbox;
    public Boss1_Hitbox boss1HitboxScript;
    public BoxCollider2D boss1HitboxCollider;
    private bool attacking;
    private float attackWindup;
    private float attackActive;
    private Vector2 attackDimensions;
    private Vector2 attackOffset;
    private int attackDamage;

    public GameObject Boss1Visuals;
    public SpriteRenderer Boss1Sprite;

    GameObject player;

    private void Start()
    {
        boss1Hitbox.SetActive(false);
        boss1HitboxVisuals.SetActive(false);
        boss1IndicatorVisuals.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    public void Boss1_ActiveUpdate()
    {
        if (actionInterval > 0 && !attacking)
        {
            direction = Vector3.zero;
            direction = (player.transform.position - transform.position).normalized;
            if (direction.x < 0) { Boss1Sprite.flipX = true; } else { Boss1Sprite.flipX = false; }
            transform.position += direction * moveSpeed * Time.deltaTime;
            actionInterval -= Time.deltaTime;
        }

        if (actionInterval <= 0)
        {
            float distCheck = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log("Player pos: " + player.transform.position + " || Boss pos: " + transform.position + "\nDistance: " + distCheck);
            if (Vector3.Distance(transform.position, player.transform.position) > chargeAttackRange)
            {
                moveSpeed = sprintSpeed;
                actionInterval = 1;
            }
            else
            {
                moveSpeed = walkSpeed;
                DetermineAttack();
                actionInterval = 3;
                Debug.Log("Taking action.");
            }
                
        }
    }

    public void DetermineAttack()
    {
        // 0 = swipe, 1 = slam, 2 = charge

        int attackValue = 0;
        attacking = true;

        if (Vector3.Distance(transform.position, player.transform.position) < meleeAttackRange)
        {
            attackValue = (UnityEngine.Random.value > 0.5f) ? 1 : 0;
        }
        else
        {
            attackValue = 2;
        }

        AttackValues(attackValue);

        Debug.Log("Attack Value: " + attackValue);
    }

    public void AttackValues(int attack)
    {
        switch (attack)
        {
            case 0:
                attackOffset = new Vector2(2, 0);
                attackDimensions = new Vector2(3.5f, 4);
                attackWindup = 0.4f;
                attackActive = 0.2f;
                boss1HitboxScript.damage = 10;
                break;
            case 1:
                attackOffset = new Vector2(0, 0);
                attackDimensions = new Vector2(5, 4);
                attackWindup = 0.9f;
                attackActive = 0.2f;
                boss1HitboxScript.damage = 15;
                break;
            case 2:
                attackOffset = new Vector2(0, 0);
                attackDimensions = new Vector2(2, 2);
                attackWindup = 0.5f;
                attackActive = 1.2f;
                boss1HitboxScript.damage = 20;
                break;
            default: break;
        }

        if (direction.x < 0)
        {
            attackOffset.x *= -1;
        }

        boss1HitboxCollider.offset = attackOffset;
        boss1HitboxCollider.size = attackDimensions;
        boss1HitboxVisuals.transform.localPosition = attackOffset;
        boss1HitboxVisuals.transform.localScale = Vector3.zero;
        boss1IndicatorVisuals.transform.localPosition = attackOffset;
        boss1IndicatorVisuals.transform.localScale = attackDimensions;

        StartCoroutine(Attack(attack, attackWindup, attackActive));
    }

    IEnumerator Attack(int attackType, float windup, float active)
    {
        float elapsed = 0f;
        float totalTime = windup + active;
        boss1HitboxScript.playerAttacked = false;
        Debug.Log("Start boss attack.");

        while (elapsed < totalTime && attacking)
        {
            boss1HitboxVisuals.SetActive(true);
            boss1IndicatorVisuals.SetActive(true);
            Vector3 tempVec3 = boss1IndicatorVisuals.transform.localScale / windup;
            
            elapsed += Time.deltaTime;
            if (boss1HitboxVisuals.transform.localScale.sqrMagnitude < boss1IndicatorVisuals.transform.localScale.sqrMagnitude)
            {
                boss1HitboxVisuals.transform.localScale += tempVec3 * Time.deltaTime;
            }
            if (attackType == 2 && elapsed < windup)
            {
                direction = (player.transform.position - transform.position).normalized;
            }
            if (elapsed >= windup)
            {
                boss1Hitbox.SetActive(true);
                if (attackType == 2)
                {
                    transform.position += direction * chargeSpeed * Time.deltaTime;
                }
            }
            yield return null;
        }
        boss1Hitbox.SetActive(false);
        boss1HitboxVisuals.SetActive(false);
        boss1IndicatorVisuals.SetActive(false);
        Debug.Log("Finished boss attack.");
        attacking = false;
        yield return null;
    }
}
