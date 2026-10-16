using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWInScene : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BackToStartMenu());
        
    }


    IEnumerator BackToStartMenu()
    {

        yield return new WaitForSeconds(6f);
        
        SceneManager.LoadScene("Main menu");
    }
  
}
