using UnityEngine;
using static IDamageable;


public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float kockBackPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy"))
        {

            if (collision.TryGetComponent<Damageable>(out Damageable damageableObject))
            {

                Vector2 pos = transform.position;
                damageableObject.Damage(damage, pos, kockBackPower);
            }

            Destroy(gameObject);
        }
    }


}
