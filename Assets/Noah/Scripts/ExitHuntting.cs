using UnityEngine;
using UnityEngine.SceneManagement;
using static IInteractable;

public class ExitHuntting : MonoBehaviour, Interactable
{
    SceneLoader sceneloader;

    void Start()
    {
        //sceneloader = Object.FindAnyObjectByType<SceneLoader>();
        
    }
    


    public void Interact()
    {
        
        //move player back to the village
        backToVillage();
    }


    void backToVillage()
    {

        // enable the village scene 
        // move player to the  village scene
        //remove the current scene - hunting scene
        //make the village scene the current scene 



        Debug.Log("Go back to village");
        //Vector2 spawnPos = new Vector2(7, 0);
        //sceneloader.LoadVillage(spawnPos);
        
        
    }
}
