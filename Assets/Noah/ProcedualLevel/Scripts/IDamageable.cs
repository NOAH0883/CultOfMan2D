using UnityEngine;

public class IDamageable : MonoBehaviour
{
    public interface Damageable
    {
        void Damage(float damage, Vector2 hitpos, float knockBackPower);

    }
}
