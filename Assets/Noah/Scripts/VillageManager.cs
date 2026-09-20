using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;



public class VillageManager : MonoBehaviour
{

    [Header("Food")]
    
    [SerializeField] int spareFood;
    [SerializeField] int sacrificeAmount;

    [Header("Villagers")]
    [SerializeField] GameObject villagerPrefab;
    public List<GameObject> villagers;
    [SerializeField] VillagerAI villagerScript;


    [Header("housing")]
    [SerializeField] float housing;
    [SerializeField] float villagersToSpawn;
    [SerializeField] float villagerSpawnMax;
    [SerializeField] GameObject HousePrefab;

    [Header("Spawner")]
    [SerializeField] List<GameObject> spawnPos;


    
    public bool hasWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        hasWeapon = false;

        GameData.isDay = true;
        GameData.population = 3;
        gameObject.SetActive(true);

        for (int i = 0; i < GameData.population; i++)
        {
            SpawnVillagerStart();
        }
        housing = 5;
    }


    

    public void Test()
    {
        Debug.Log("work plz");
        GameData.food += spareFood; //add spare food to current food 

        villagers = villagers.OrderByDescending(go => go.GetComponent<VillagerAI>().isSick).ToList(); // sort the list so that the sick villagers get feed first

        bool villagersSick = false;

        for (int i = 0; i < villagers.Count; i++)
        {
            VillagerAI testVillager = villagers[i].GetComponent<VillagerAI>();
            if (GameData.food >= testVillager.foodNeeded)
            {
                GameData.food -= testVillager.foodNeeded;
                testVillager.isSick = false;
                
                SpriteRenderer sr = testVillager.GetComponent<SpriteRenderer>();
                sr.color = Color.white;

                continue;
            }

            if (testVillager.isSick != true)
            {
                testVillager.isSick = true;
                villagersSick = true;
                
                SpriteRenderer sr = testVillager.GetComponent<SpriteRenderer>();
                sr.color = Color.green;

                continue;
            }
            else
            {
                Destroy(villagers[i]);
                villagers.RemoveAt(i);
                GameData.population--;
            }
        }

       
         
    }

    
    void OverNight()
    {
        //if (GameData.food >= Mathf.RoundToInt(GameData.population / 2))
        //    spareFood = Mathf.RoundToInt(GameData.population / 2);
        //else
        //    spareFood = Mathf.RoundToInt(GameData.food);


        //if (GameData.population < housing && !villagersSick)
        //    SpawnVillager();
    }



    
    private void SpawnVillagerStart()
    {

        int randSpawn = Random.Range(0, spawnPos.Count);
        Debug.Log(randSpawn);
        Transform spawnPoint = spawnPos[randSpawn].transform;

        GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);
        
        villagers.Add(villager);
        DontDestroyOnLoad(villager);
    }


    
    public void SpawnVillager()
    {
        
        villagersToSpawn = housing - GameData.population;
        
        if (villagersToSpawn > 3)
            villagersToSpawn = 3;

        float rnd = Random.Range(1, villagersToSpawn);


        for (int i = 0; i < rnd; i++)
        {
            int randSpawn = Random.Range(0, spawnPos.Count);
            Transform spawnPoint = spawnPos[randSpawn].transform;
            GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);

            villagers.Add(villager);
            DontDestroyOnLoad(villager);
            GameData.population++;
        }
    }


    //public void SpawnHouse()
    //{
    //    housing += 5;
    //    GameObject house = Instantiate(HousePrefab, transform.position, Quaternion.identity);
    //}



    public void Sacrifice()
    {
        int rnd = Random.Range(0, villagers.Count);  
        Destroy(villagers[rnd]);  
        villagers.RemoveAt(rnd);
        GameData.population--;
        GameData.food += sacrificeAmount;
    }


}
