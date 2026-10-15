using UnityEngine;

public class VIllageMusic : MonoBehaviour
{
    [SerializeField] AudioClip dayVillageMusic;
    [SerializeField] AudioClip nightVillageMusic;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.isDay)
        {
            audioSource.clip = dayVillageMusic; 
            audioSource.Play();
        }
        else
        {
            audioSource.clip = nightVillageMusic;
            audioSource.Play();
        }
    }
}
