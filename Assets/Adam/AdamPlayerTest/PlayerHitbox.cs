using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public int damage = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boss hit for " + damage + " damage.");
    }
}
