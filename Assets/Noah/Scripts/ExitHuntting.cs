using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement;
using static IInteractable;

public class ExitHuntting : MonoBehaviour, Interactable
{
    SceneLoader sceneloader;
    [SerializeField] bool village;

    void Start()
    {
       sceneloader = GetComponent<SceneLoader>();
        
    }
    


    public void Interact()
    {
        if (village)
        {
            backToVillage();
        }
        else if (!village)
        {
            ToBossRoom();
        }

    }

    void ToBossRoom()
    {
        sceneloader.LoadBoss();
    }

    void backToVillage()
    {
        
        sceneloader.LoadVillage();
        
    }


}
