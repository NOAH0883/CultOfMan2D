using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;




public class VillageManager : MonoBehaviour
{

    [Header("Food")]
    [SerializeField] int population;
    public float currentFood;
    [SerializeField] int spareFood;
    [SerializeField] int sacrificeAmount;

    [Header("Villagers")]
    [SerializeField] GameObject villagerPrefab;
    [SerializeField] List<GameObject> villagers;
    [SerializeField] VillagerAI villagerScript;
    

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
        currentFood += spareFood; //add spare food to current food 

        villagers = villagers.OrderByDescending(go => go.GetComponent<VillagerAI>().isSick).ToList(); // sort the list so that the sick villagers get feed first

        bool villagersSick = false;

        for (int i = 0; i < villagers.Count; i++)
        {
            VillagerAI testVillager = villagers[i].GetComponent<VillagerAI>();
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
                Destroy(villagers[i]);
                villagers.RemoveAt(i);
                
            }
        }

        if (currentFood >= Mathf.RoundToInt(population / 2) )
            spareFood = Mathf.RoundToInt(population / 2);
        else
            spareFood = Mathf.RoundToInt(currentFood);
        
        
        if (population < housing && !villagersSick)
            SpawnVillager();
         
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

        float rnd = Random.Range(1, villagersToSpawn);


        for (int i = 0; i < rnd; i++)
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



    public void Sacrifice()
    {
        int rnd = Random.Range(0, villagers.Count);  
        Destroy(villagers[rnd]);  
        villagers.RemoveAt(rnd);
        population--;
        currentFood += sacrificeAmount;
    }
}
