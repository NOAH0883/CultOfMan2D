using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int spawnAmount;
    [SerializeField] float enemiesSpawned;
    [SerializeField] float range;

    [SerializeField] GameObject spawnObject;
    

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        
        return false;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // spawn amount is random number, food needed x 1.5 ( +/- either side)
        if (enemiesSpawned < spawnAmount) 
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                int randomEnemy = Random.Range(0, enemyPrefabs.Count);
                Instantiate(enemyPrefabs[randomEnemy], point, Quaternion.identity);

                Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                enemiesSpawned++;
            }
        }
        
    }

}
