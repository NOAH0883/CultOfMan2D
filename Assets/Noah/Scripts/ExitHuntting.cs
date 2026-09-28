using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static IInteractable;

public class ExitHuntting : MonoBehaviour, Interactable
{
    SceneLoader sceneloader;
    
    Vector2 spawnPos;

    void Start()
    {
       sceneloader = GetComponent<SceneLoader>();
        
    }
    


    public void Interact()
    { 
        //move player back to the village
        backToVillage();
    }



    void backToVillage()
    {
        Debug.Log("Load village");
        sceneloader.LoadVillage();
        
    }


}
