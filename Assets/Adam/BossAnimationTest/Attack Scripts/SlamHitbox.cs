using UnityEngine;

public class SlamHitbox : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player hit.");
    }
}
