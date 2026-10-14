
using UnityEngine;


public enum SoundType
{ 
    interact,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] soundList;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        

        audioSource = GetComponent<AudioSource>();
    }

   

    public static void PlaySound(SoundType sound,float volume = 1f)
    {
        Instance.audioSource.PlayOneShot(Instance.soundList[(int)sound], volume);
        Debug.Log("==Play Sound" + sound);
    }
}
