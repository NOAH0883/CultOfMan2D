using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class VillageManager : MonoBehaviour
{



    [Header("Food")]
    [SerializeField] int population;
    [SerializeField] int currentFood;
    [SerializeField] int spareFood;

    [Header("Villagers")]
    [SerializeField] GameObject villagerPrefab;
    [SerializeField] List<GameObject> villagers;
    [SerializeField] TestVillager villagerScript;

    [Header("housing")]
    [SerializeField] float housing;
    [SerializeField] float villagersToSpawn;
    [SerializeField] float villagerSpawnMax;
    [SerializeField] GameObject HousePrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            SpawnVillagerStart();
        }
    }

    public void Test()
    {
        villagers = villagers.OrderByDescending(go => go.GetComponent<TestVillager>().isSick).ToList(); // sort the list so that the sick villagers get feed first

        for (int i = 0; i < villagers.Count; i++)
        {
            TestVillager testVillager = villagers[i].GetComponent<TestVillager>();
            if (currentFood >= testVillager.foodNeeded)
            {
                currentFood -= testVillager.foodNeeded;
                testVillager.isSick = false;
                continue;
            }

            if (testVillager.isSick != true)

                testVillager.isSick = true;
            else
            {
                villagers.RemoveAt(i);
                Destroy(villagers[i]);
            }
        }

        spareFood = currentFood;
        
        
        if (population < housing)
        {
            SpawnVillager();
        }
            
    }

    
    
    private void SpawnVillagerStart()
    {
        GameObject villager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity);
        villagers.Add(villager);
    }


   
    
    private void SpawnVillager()
    {
        housing = 10;

        villagersToSpawn = housing - population;

        for (int i = 0; i < villagersToSpawn; i++)
        {
            GameObject villager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity);
            villagers.Add(villager);
        }
       

        // house can hold 5 villagers
        // if villagers are feed 
        // check if population is less than housing 
        //housing - population = villagersToSpawn
        //spawnMax = housing
        //if villagersToSpawn is greater than spawnMax
        //villagersToSpawn = spawnMax
        //for villagersToSpawn
        // spawn villagers and add to list 
    }


    public void SpawnHouse()
    {
        housing += 5;

        float randPosx = Random.Range(5, -5);
        float randPosy = Random.Range(5, -5);
        Vector2 housingPos = new Vector2(randPosx, randPosy);

        GameObject house = Instantiate(HousePrefab, housingPos, Quaternion.identity);
    }
}
