using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class VillageManager : MonoBehaviour
{

    [SerializeField] int population;
    [SerializeField] int currentFood;
    [SerializeField] int spareFood;
    [SerializeField] GameObject villagerPrefab;

    [SerializeField] List<GameObject> villagers;

    [SerializeField] TestVillager villagerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < population; i++)
        {
            SpawnVillager();
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
    }

    private void SpawnVillager()
    {
        GameObject villager = Instantiate(villagerPrefab, Vector3.zero, Quaternion.identity);
        villagers.Add(villager);
    }


}
