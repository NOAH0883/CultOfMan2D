using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int damage = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit for " + damage + " damage.");
            PassiveEnemy enemy = collision.GetComponent<PassiveEnemy>();
            enemy.TakeDamage(damage);
        }
        if (collision.CompareTag("Boss1"))
        {
            Boss1_Manager boss = collision.GetComponentInParent<Boss1_Manager>();
            boss.TakeDamage(damage);
        }
        
    }
}
