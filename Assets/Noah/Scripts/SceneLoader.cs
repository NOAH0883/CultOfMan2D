
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SceneLoader : MonoBehaviour
{
    
    [SerializeField] List<GameObject> keepLoaded;

    public void LoadHunting(string huntingLevel)
    {
        StartCoroutine(Hunting(huntingLevel));
    }

    public void LoadVillage()
    {
        StartCoroutine(Village());
    }






    IEnumerator Hunting(string huntingLevel)
   {
        // load hunting scene 

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(huntingLevel, LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }
        
        string currentScene = SceneManager.GetActiveScene().name;
        Scene villageScene = SceneManager.GetSceneByName(currentScene);

        Scene huntingScene = SceneManager.GetSceneByName(huntingLevel);
        SceneManager.SetActiveScene(huntingScene);


        
        for (int i = 0; i < keepLoaded.Count; i++)
        {
            SceneManager.MoveGameObjectToScene(keepLoaded[i], huntingScene);
        }


        GameObject[] rootObjects = villageScene.GetRootGameObjects();
        
        foreach (GameObject rootObj in rootObjects)
        {
            rootObj.SetActive(false);
        }
        
    }


    IEnumerator Village()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Scene HunitngScene = SceneManager.GetSceneByName(currentScene);


        Scene villageScene = SceneManager.GetSceneByName("Village");
        SceneManager.SetActiveScene(villageScene);

        
        GameObject[] rootObjects = villageScene.GetRootGameObjects();

        foreach (GameObject rootObj in rootObjects)
        {
            rootObj.SetActive(true);
        }


        for (int i = 0; i < keepLoaded.Count; i++)
        {
            SceneManager.MoveGameObjectToScene(keepLoaded[i], villageScene);
        }

        SceneManager.UnloadSceneAsync(HunitngScene);
        
        yield return null;
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



}
