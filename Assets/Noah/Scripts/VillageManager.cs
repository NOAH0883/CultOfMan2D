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
    [SerializeField] VillagersMovement villager;
    

    [Header("housing")]
    [SerializeField] float housing;
    [SerializeField] float villagersToSpawn;
    [SerializeField] float villagerSpawnMax;
    [SerializeField] GameObject HousePrefab;

    [Header("Spawner")]
    [SerializeField] List<GameObject> spawnPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            SpawnVillagerStart();
        }
        housing = 5;
    }

    public void Test()
    {

        villagers = villagers.OrderByDescending(go => go.GetComponent<TestVillager>().isSick).ToList(); // sort the list so that the sick villagers get feed first

        //bool villagersSick?
        //set villagersSick = false
        // if villager is sick, villagersSick = true

        bool villagersSick = false;


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
            {
                testVillager.isSick = true;
                villagersSick = true;
            }

            else
            {
                villagers.RemoveAt(i);
                Destroy(villagers[i]);
            }
        }

        spareFood = currentFood;
        
        
        if (population < housing && !villagersSick)
        {
            SpawnVillager();
        }
            
    }

    
    
    private void SpawnVillagerStart()
    {
        int randSpawn = Random.Range(0, spawnPos.Count);
        Debug.Log(randSpawn);
        Transform spawnPoint = spawnPos[randSpawn].transform;

        GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);
        
        villagers.Add(villager);
    }


   
    
    public void SpawnVillager()
    {
        
        villagersToSpawn = housing - population;
        
        if (villagersToSpawn > 3)
            villagersToSpawn = 3;


        for (int i = 0; i < villagersToSpawn; i++)
        {
            int randSpawn = Random.Range(0, spawnPos.Count);
            Transform spawnPoint = spawnPos[randSpawn].transform;
            GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);

            villagers.Add(villager);
            population++;
        }
    }


    public void SpawnHouse()
    {
        housing += 5;

        GameObject house = Instantiate(HousePrefab, transform.position, Quaternion.identity);
    }
}
