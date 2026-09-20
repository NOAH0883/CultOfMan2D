
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] VillageManager villageManager;
    Player player;

    public void LoadHunting(string loadScene, Vector2 spawnPos)
    {

        player = UnityEngine.Object.FindAnyObjectByType<Player>();


        for (int i = 0; i < villageManager.villagers.Count; i++)
        {
            villageManager.villagers[i].SetActive(false); // disable all villages , make sure that have dont destroy on load 
        }

        player.transform.position = spawnPos;

        SceneManager.LoadScene(loadScene);
    }

    public void LoadVillage(Vector2 spawnPos)
    {
        player = UnityEngine.Object.FindAnyObjectByType<Player>();

        for (int i = 0; i < villageManager.villagers.Count; i++)
        {
            villageManager.villagers[i].SetActive(true); 
        }

        player.transform.position = spawnPos;
        SceneManager.LoadScene("Village");
    }

}



    // go back to village
    // move back all keep gameobject
    //unload hunting scene
    //re-activate village scene
    // set time of day to allow for actions




    //static variables 
    //timeofday 
    //foodamount
    //number of villages 
    //housing 
    //wepon upgrades




