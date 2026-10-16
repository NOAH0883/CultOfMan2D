using UnityEngine;
using UnityEngine.Audio;

public class VillageMusic : MonoBehaviour
{
    [SerializeField] private AudioClip dayVillageMusic;
    [SerializeField] private AudioClip nightVillageMusic;

    private AudioSource audioSource;
    private bool audioSwitcher;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
        audioSwitcher = GameData.isDay;
        audioSource.clip = audioSwitcher ? dayVillageMusic : nightVillageMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
       
        if (GameData.isDay != audioSwitcher)
        {
            audioSwitcher = GameData.isDay;
            SwitchMusic(audioSwitcher ? dayVillageMusic : nightVillageMusic);
        }
    }

    private void SwitchMusic(AudioClip newClip)
    {
        if (audioSource.clip == newClip && audioSource.isPlaying) return;

        audioSource.clip = newClip;
        audioSource.Play();
    }

}
