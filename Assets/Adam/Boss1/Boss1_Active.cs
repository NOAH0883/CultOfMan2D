using UnityEngine;

public class Boss1_Active : MonoBehaviour
{
    public float actionInterval;

    public void Boss1_ActiveUpdate()
    {
        if (actionInterval > 0)
        {
            actionInterval -= Time.deltaTime;
        }

        if (actionInterval <= 0)
        {
            actionInterval = 3;
            Debug.Log("Taking action.");
        }
    }
}
