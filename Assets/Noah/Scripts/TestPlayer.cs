using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDamageable;

public class TestPlayer : MonoBehaviour, Damageable
{
    [SerializeField] float playerSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;

    [SerializeField] float playerHealth;
    

    bool isKnockedBack;

    [SerializeField] LayerMask enemyLayer;
    float damage = 1;
    float knockbackpower = 10;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    void OnAttack(InputValue inputValue)
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1.5f, enemyLayer);
        if (hit != null && hit.TryGetComponent<Damageable>(out Damageable damageableObject))
        {
            Debug.Log("attack enemy");
            Vector2 pos = rb.position;
            damageableObject.Damage(damage, pos, knockbackpower);
        }

    }

    void FixedUpdate()
    {
        if (isKnockedBack) return;
        rb.MovePosition(rb.position + movementInput * playerSpeed * Time.deltaTime);
    }


    public void Damage(float damage, Vector2 hitPos, float knockBackPower)
    {
        StartCoroutine(KnockBack(knockBackPower, hitPos));

        playerHealth -= damage;

        if (playerHealth <= 0)
            Debug.Log("--Dead--");
    }

    IEnumerator KnockBack(float knockBackPower, Vector2 hitPos)
    {
        isKnockedBack = true;

        Debug.Log("--knockBack--");
        Vector2 KnockBackDir = (rb.position - hitPos).normalized;
        rb.linearVelocity = Vector3.zero;

        rb.AddForce(KnockBackDir * knockBackPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.25f);
        rb.linearVelocity = Vector3.zero;
        isKnockedBack = false;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellowGreen;
        Gizmos.DrawWireSphere(transform.position, 1.5f);

        
    }



}
