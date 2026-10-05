using Unity.Cinemachine;
using UnityEngine;

public class HuntingCam : MonoBehaviour
{


    CinemachineCamera cam;
    AdamPlayerTest player;
    TestPlayer testPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        player = FindAnyObjectByType<AdamPlayerTest>();
        testPlayer = FindAnyObjectByType<TestPlayer>();
        
        
        if(player != null)
        {
            cam.Follow = player.transform;
        }
        else
        {
            cam.Follow = testPlayer.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
