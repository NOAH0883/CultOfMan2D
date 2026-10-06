using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static IDamageable;

public class TestPlayer : MonoBehaviour, Damageable
{
    [SerializeField] float playerSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;

    [SerializeField] float playerHealth;
    Vector2 KnockBackDir;
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


    void FixedUpdate()
    {
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

        Debug.Log("--knockBack--");
        KnockBackDir = hitPos - rb.position ;
        //rb.linearVelocity = Vector3.zero;

        rb.AddForce(KnockBackDir * knockBackPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1);
        //rb.linearVelocity = Vector3.zero;


        yield return null;
    }

}
