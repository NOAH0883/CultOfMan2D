using UnityEngine;
using UnityEngine.SceneManagement;
using static IInteractable;

public class huntingManager : MonoBehaviour, Interactable
{
    SceneLoader sceneloader;

    void Start()
    {
        sceneloader = Object.FindAnyObjectByType<SceneLoader>();
        
    }
    


    public void Interact()
    {
        
        //move player back to the village
        backToVillage();
    }


    void backToVillage()
    {
        Vector2 spawnPos = new Vector2(7, 0);
        
        sceneloader.LoadVillage(spawnPos);
        
        
    }
}
