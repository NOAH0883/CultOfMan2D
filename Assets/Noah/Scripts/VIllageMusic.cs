using UnityEngine;

public class VIllageMusic : MonoBehaviour
{
    [SerializeField] AudioClip dayVillageMusic;
    [SerializeField] AudioClip nightVillageMusic;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.isDay)
        {
            audioSource.clip = dayVillageMusic; 
            
        }
        else
        {
            audioSource.clip = nightVillageMusic;
            
        }
    }
}
