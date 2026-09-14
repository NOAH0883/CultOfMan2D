using System.Collections;
using UnityEngine;

public class BossAnimationTest : MonoBehaviour
{
    public GameObject slamHitbox;
    void Start()
    {
        slamHitbox.SetActive(false);
    }
    void BossAnimationEvent()
    {
        Debug.Log("Slam attack.");
        StartCoroutine(SlamAttack());
    }
    IEnumerator SlamAttack()
    {
        slamHitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slamHitbox.SetActive(false);
    }
}
