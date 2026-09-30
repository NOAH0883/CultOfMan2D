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
        Debug.Log("Load scene");
        //move player back to the village
        backToVillage();
    }



    void backToVillage()
    {
        
        sceneloader.LoadVillage();
        
    }


}
