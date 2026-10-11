using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;



public class VillageManager : MonoBehaviour
{

    [Header("Food")]
    int foodInPot;
    
    [SerializeField] int sacrificeAmount;

    [Header("Villagers")]
    [SerializeField] GameObject villagerPrefab;
    public List<GameObject> villagers;
    [SerializeField] VillagerAI villagerScript;
    public int villagersFeed;
    bool canSpawnVillagers;


    [Header("housing")]
    public float housing;
    float housesToBuild;
    [SerializeField] List<GameObject> housesGameObj;
    


    [Header("Spawner")]
    [SerializeField] List<GameObject> spawnPos;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GameData.isDay = true;
        GameData.food = 0;


        GameData.hasWeapon = false;

        GameData.isDay = true;
        GameData.population = 3;
        gameObject.SetActive(true);

        for (int i = 0; i < GameData.population; i++)
        {
            SpawnVillagerStart();
        }
        
        housing = 5;

    }


    

    public void FeedVillagers()
    {
        villagers = villagers.OrderByDescending(go => go.GetComponent<VillagerAI>().isSick).ToList(); // sort the list so that the sick villagers get feed first


        for (int i = 0; i < villagers.Count; i++)
        {
            
            VillagerAI testVillager = villagers[i].GetComponent<VillagerAI>();

            if (GameData.food >= testVillager.foodNeeded)
            {
                GameData.food -= testVillager.foodNeeded;
                testVillager.isSick = false;
                
                SpriteRenderer sr = testVillager.GetComponent<SpriteRenderer>();
                sr.color = Color.white;
                canSpawnVillagers = true;
                continue;
            }

            if (testVillager.isSick != true)
            {
                testVillager.isSick = true;
                canSpawnVillagers = false;

                SpriteRenderer sr = testVillager.GetComponent<SpriteRenderer>();
                sr.color = Color.green;
            }
            else
            {
                Destroy(villagers[i]);
                villagers.RemoveAt(i);
                GameData.population--;
                canSpawnVillagers = false;
            }
        }
    }


    public void BuildHouses()
    {
        if(housesToBuild > 0)
        {
            for (int i = 0; i < housesToBuild; i++)
            {
                housesGameObj[0].SetActive(true);
                housesGameObj.RemoveAt(0);
            }
        }
    }

    public void SpawnVillages()
    {
       if (canSpawnVillagers && housesGameObj != null)
       {
            if (housing - GameData.population >=2)
            {
                float villagersToSpawn = Random.Range(0, 3);
                SpawnVillager(villagersToSpawn);
            }
            else if(housing - GameData.population > 0)
            {
                float villagersToSpawn = 1;
                SpawnVillager(villagersToSpawn);
            }
       }

    }



   
    
    private void SpawnVillagerStart()
    {

        int randSpawn = Random.Range(0, spawnPos.Count);
        
        Transform spawnPoint = spawnPos[randSpawn].transform;

        GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);
        
        villagers.Add(villager);
        //DontDestroyOnLoad(villager);
        
        Debug.Log(villagers.Count);
    }


    
    void SpawnVillager(float villagersToSpawn)
    {
        for (int i = 0; i < villagersToSpawn; i++)
        {
            int randSpawn = Random.Range(0, spawnPos.Count);
            Transform spawnPoint = spawnPos[randSpawn].transform;
            GameObject villager = Instantiate(villagerPrefab, spawnPoint.position, Quaternion.identity);

            villagers.Add(villager);
            GameData.population++;
        }
    }



    public void FoodInPot()
    {
        foodInPot += 4;
        GameData.food -= 4;
        villagersFeed = Mathf.Clamp(foodInPot / 2, 0, GameData.population);
    }
    
    public void upgradeHousing()
    {
        housing += 5;
        housesToBuild++;
    }


    public void Sacrifice()
    {
        int rnd = Random.Range(0, villagers.Count);
        Debug.Log(rnd);
        Destroy(villagers[rnd]);
        villagers.RemoveAt(rnd);

        GameData.population--;
        GameData.food += sacrificeAmount;
    }


}
