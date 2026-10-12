using Mono.Cecil.Cil;
using UnityEngine;
using static IDamageable;

public class PlayerHitbox : MonoBehaviour
{
    public float damage;
    public float knockBackPower;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out Damageable damageableObject))
        {
            Vector2 pos = transform.position;
            damageableObject.Damage(damage, pos, knockBackPower);
        }

    }
}
