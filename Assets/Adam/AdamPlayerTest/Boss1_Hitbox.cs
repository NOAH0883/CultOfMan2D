using UnityEngine;

public class Boss1_Hitbox : MonoBehaviour
{
    public int damage;
    public bool playerAttacked = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AdamPlayerTest player = collision.GetComponentInParent<AdamPlayerTest>();
            if (!player.dodging && !playerAttacked)
            {
                Debug.Log("Player hit for " + damage + " damage.");
                playerAttacked = true;
                player.TakeDamage(damage);
            }
            
        }
    }
}
