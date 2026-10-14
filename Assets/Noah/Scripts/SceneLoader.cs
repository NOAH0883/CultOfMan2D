
using System.Collections;
using Unity.VisualScripting;
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
        GameData.isDay = true;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector2(0, 0);

        TutorialManager tutorialManager = player.GetComponentInChildren<TutorialManager>();
        if (tutorialManager != null)
        {
            tutorialManager.nextTutorial();
            Time.timeScale = 0f;
        }

        Scene villageScene = SceneManager.GetSceneByName("Day1_tutorial 1");
        
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

        GameData.isDay = false;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector2(9, 0);

        Scene activeScene = SceneManager.GetActiveScene();
        Scene villageScene = SceneManager.GetSceneByName("Day1_tutorial 1");

        TutorialManager tutorialManager = player.GetComponentInChildren<TutorialManager>();
        if (tutorialManager != null)
        {
            tutorialManager.nextTutorial();
            Time.timeScale = 0f;

        }

        //turn on all the gameobjects in the village scene
        foreach (GameObject rootObj in villageScene.GetRootGameObjects())
        {
            rootObj.SetActive(true);
            
        }

        SceneManager.UnloadSceneAsync(activeScene);

        yield return null;
    }

  

}







