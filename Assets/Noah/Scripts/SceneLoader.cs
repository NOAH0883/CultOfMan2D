using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHunting(string loadScene)
    {
        //if (!GameData.tutorialDone)
        //{
        //    TutorialManager tutorialManager = FindAnyObjectByType<TutorialManager>();
        //    if (tutorialManager != null)
        //    {
        //        tutorialManager.NextTutorial();
        //    }
        //}

        


        StartCoroutine(LoadHuntingScene(loadScene));
    }

    public void LoadVillage()
    {

        //if (!GameData.tutorialDone)
        //{
        //    TutorialManager tutorialManager = FindAnyObjectByType<TutorialManager>();
        //    if (tutorialManager != null)
        //    {
        //        tutorialManager.NextTutorial();
        //    }
        //}
        StartCoroutine(LoadVillageScene());
    }

    public void LoadBoss()
    {

        StartCoroutine(LoadBossScene());
    }

    IEnumerator LoadBossScene()
    {
        yield return 0;
        Scene activeScene = SceneManager.GetActiveScene();

        AsyncOperation async = SceneManager.LoadSceneAsync("Boss", LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }

        Scene bossScene = SceneManager.GetSceneByName("Boss");
        if (bossScene.IsValid())
        {
            SceneManager.SetActiveScene(bossScene);
            
        }

        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector2(0, 0);
        }

        if (activeScene.IsValid() && activeScene.name != "Boss")
        {
            yield return SceneManager.UnloadSceneAsync(activeScene);
        }

       
    }

    IEnumerator LoadHuntingScene(string loadScene)
    {
        yield return 0;

        Scene villageScene = SceneManager.GetSceneByName("MainGame_Village");
        GameObject[] villageRootObjects = villageScene.IsValid() ? villageScene.GetRootGameObjects() : new GameObject[0];

        AsyncOperation async = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }

        Scene huntedScene = SceneManager.GetSceneByName(loadScene);
        if (huntedScene.IsValid())
        {
            SceneManager.SetActiveScene(huntedScene);
        }


        

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Move player to 0");
            player.transform.position = new Vector2(0, 0);
        }

        foreach (GameObject rootObj in villageRootObjects)
        {
            if (rootObj != null) rootObj.SetActive(false);
        }

        GameData.isDay = true;

        
    }

    IEnumerator LoadVillageScene()
    {
        yield return 0;

        Scene activeScene = SceneManager.GetActiveScene();
        
       
        Scene villageScene = SceneManager.GetSceneByName("MainGame_Village");

        if (villageScene.IsValid())
        {
            foreach (GameObject rootObj in villageScene.GetRootGameObjects())
            {
                if (rootObj != null) rootObj.SetActive(true);
            }

            
            SceneManager.SetActiveScene(villageScene);
        }
        
        GameData.isDay = false;

        


        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector2(9, 0);
        }

        
        if (activeScene.IsValid() && activeScene.name != "MainGame_Village")
        {
            yield return SceneManager.UnloadSceneAsync(activeScene);
        }
    }
}
