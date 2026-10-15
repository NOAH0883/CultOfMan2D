using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class Boss1_Idle : MonoBehaviour
{
    Boss1_Manager Boss1_Manager;
    public int movingToPos = 0;
    private Vector2 startPosition;
    public Vector2 patrolPos1;
    public Vector2 patrolPos2;
    public float walkSpeed;
    public float idleTime;
    [SerializeField] private float waitTime;

    public GameObject boss1Visuals;
    private Boss1_VisualsScript boss1VisualsScript;
    public SpriteRenderer Boss1Sprite;

    void Start()
    {
        movingToPos = 0;
        startPosition = transform.position;
        patrolPos1 = startPosition + patrolPos1;
        patrolPos2 = startPosition + patrolPos2;
        boss1VisualsScript = boss1Visuals.GetComponent<Boss1_VisualsScript>();
    }

    public void Boss1_IdleUpdate()
    {
        if (idleTime <= 0)
        {
            boss1VisualsScript.moving = true;
            Vector3 direction = Vector3.zero;
            switch (movingToPos)
            {
                case 0: direction = (new Vector3(startPosition.x, startPosition.y, 0) - transform.position).normalized; break;
                case 1: direction = (new Vector3(patrolPos1.x, patrolPos1.y, 0) - transform.position).normalized; break;
                case 2: direction = (new Vector3(patrolPos2.x, patrolPos2.y, 0) - transform.position).normalized; break;
                default: break;
            }

            if (movingToPos == 0 && Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                idleTime = waitTime;
                movingToPos = 1;
                Debug.Log("movingToPos = 1.");
            }

            if (movingToPos == 1 && Vector3.Distance(transform.position, patrolPos1) < 0.1f)
            {
                idleTime = waitTime;
                movingToPos = 2;
                Debug.Log("movingToPos = 2.");
            }

            if (movingToPos == 2 && Vector3.Distance(transform.position, patrolPos2) < 0.1f)
            {
                idleTime = waitTime;
                movingToPos = 1;
                Debug.Log("movingToPos = 1.");
            }

            if (direction.x < 0) { Boss1Sprite.flipX = true; } else { Boss1Sprite.flipX = false; }
            transform.position += direction * walkSpeed * Time.deltaTime;
        }
        else
        {
            boss1VisualsScript.moving = false;
            if (patrolPos1 != startPosition && patrolPos2 != startPosition)
            {
                idleTime -= Time.deltaTime;
            }
        }
        
    }

    public void CompareDistance()
    {
        float startDist = Vector3.Distance(transform.position, startPosition);
        float pos1Dist = Vector3.Distance(transform.position, patrolPos1);
        float pos2Dist = Vector3.Distance(transform.position, patrolPos2);

        float[] patrolDistances = { startDist, pos1Dist, pos2Dist };

        float closestDistance = patrolDistances.Min();

        int index = Array.IndexOf(patrolDistances, closestDistance);

        movingToPos = index;
        
    }
}
