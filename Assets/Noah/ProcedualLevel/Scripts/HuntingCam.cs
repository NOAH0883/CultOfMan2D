using Unity.Cinemachine;
using UnityEngine;

public class HuntingCam : MonoBehaviour
{


    CinemachineCamera cam;
    AdamPlayerTest player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        player = FindAnyObjectByType<AdamPlayerTest>();

        cam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
