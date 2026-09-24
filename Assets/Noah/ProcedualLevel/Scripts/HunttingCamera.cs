using Unity.VisualScripting;
using UnityEngine;

public class HunttingCamera : MonoBehaviour
{

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set camrea to piority
        //turn on cam
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //remove camera from priority
        //turn off cam
    }

}
