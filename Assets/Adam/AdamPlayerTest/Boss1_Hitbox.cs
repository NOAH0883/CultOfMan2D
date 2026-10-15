using UnityEngine;
using static IDamageable;

public class Boss1_Hitbox : MonoBehaviour
{
    public int damage;
    public float knockBackPower;
    public bool playerAttacked = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        AdamPlayerTest player = collision.GetComponentInParent<AdamPlayerTest>();

        if (player == null) return;

        if (collision.TryGetComponent<Damageable>(out Damageable damageableObject))
        {
            
            Vector2 pos = transform.position;
            if (!player.dodging && !playerAttacked)
            {
                Debug.Log("Player hit for " + damage + " damage.");
                damageableObject.Damage(damage, pos, knockBackPower);
                playerAttacked = true;
            }
            
        }

    }
}
