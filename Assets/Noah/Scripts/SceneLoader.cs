
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{
    
   

    public void LoadHunting(string loadScene)
    {
        StartCoroutine(LoadHuntingScene(loadScene));
    }

    public void LoadVillage()
    {
        //GameData.isDay = false;
        StartCoroutine(LoadVillageScene());

    }


    IEnumerator LoadHuntingScene(string loadScene)
    {

        Scene villageScene = SceneManager.GetSceneByName("VIllage");
        
        AsyncOperation async = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return null;
        }

        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadScene));


        foreach (GameObject rootObj in villageScene.GetRootGameObjects())
        {
            rootObj.SetActive(false);
            
        }
  
    }



    IEnumerator LoadVillageScene()
    {
        

        Scene activeScene = SceneManager.GetActiveScene();

        Scene villageScene = SceneManager.GetSceneByName("VIllage");

        

        //turn on all the gameobjects in the village scene
        foreach (GameObject rootObj in villageScene.GetRootGameObjects())
        {
            rootObj.SetActive(true);
            
        }

        SceneManager.UnloadSceneAsync(activeScene);

        yield return null;
    }

  

}







