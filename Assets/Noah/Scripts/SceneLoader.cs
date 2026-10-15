
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour
{


    public void LoadHunting(string loadScene)
    {
        TutorialManager tutorialManager = FindAnyObjectByType<TutorialManager>();
        if (tutorialManager != null && !tutorialManager.tutorialDone)
        {
            tutorialManager.nextTutorial();
        }

        StartCoroutine(LoadHuntingScene(loadScene));
    }

    public void LoadVillage()
    {
        TutorialManager tutorialManager = FindAnyObjectByType<TutorialManager>();
        if (tutorialManager != null && !tutorialManager.tutorialDone)
        {
            tutorialManager.nextTutorial();
        }
        
        StartCoroutine(LoadVillageScene());
    }



    public void LoadBoss()
    {
        StartCoroutine(LoadBossScene());
    }


    IEnumerator LoadBossScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        AsyncOperation async = SceneManager.LoadSceneAsync("Boss", LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Boss"));

        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector2(0, 0);


    }



    IEnumerator LoadHuntingScene(string loadScene)
    {

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

        GameData.isDay = true;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector2(0, 0);
    }



    IEnumerator LoadVillageScene()
    {


        Scene activeScene = SceneManager.GetActiveScene();
        Scene villageScene = SceneManager.GetSceneByName("Day1_tutorial 1");

        

        //turn on all the gameobjects in the village scene
        foreach (GameObject rootObj in villageScene.GetRootGameObjects())
        {
            rootObj.SetActive(true);
            
        }

        GameData.isDay = false;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector2(9, 0);

        SceneManager.UnloadSceneAsync(activeScene);

        yield return null;
    }

  

}







