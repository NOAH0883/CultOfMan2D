using UnityEngine;
using UnityEngine.SceneManagement;
using static IInteractable;

public class huntingManager : MonoBehaviour, Interactable
{
    SceneLoader sceneloader;

    void Start()
    {
        sceneloader = Object.FindAnyObjectByType<SceneLoader>();
        if (sceneloader != null)
            Debug.Log("got the sceneloader");
    }
    


    public void Interact()
    {
        
        //move player back to the village
        backToVillage();
    }


    void backToVillage()
    {
        Vector2 spawnPos = new Vector2(7, 0);
        GameData.isDay = false;
        sceneloader.LoadVillage(spawnPos);
        
        
    }
}
