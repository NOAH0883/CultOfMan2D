using UnityEngine;

public class Boss1_Active : MonoBehaviour
{
    public float actionInterval;
    public float walkSpeed;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void Boss1_ActiveUpdate()
    {
        if (actionInterval > 0)
        {
            Vector3 direction = Vector3.zero;
            direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * walkSpeed * Time.deltaTime;
            actionInterval -= Time.deltaTime;
        }

        if (actionInterval <= 0)
        {
            actionInterval = 3;
            Debug.Log("Taking action.");
        }
    }
}
