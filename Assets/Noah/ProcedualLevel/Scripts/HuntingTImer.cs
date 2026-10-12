using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HuntingTImer : MonoBehaviour
{

    [SerializeField] float timer;
    float lightIntensity;

    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Light2D Light;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Light.intensity = 1;
        lightIntensity = 1; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer !>= 0)
        {
            timer -= Time.deltaTime;
            lightIntensity -= Time.deltaTime;
            Light.intensity = Mathf.Clamp(timer/50, 0.03f, 1);
        }
        else
        {
            sceneLoader.LoadVillage();
        }





    }
}
