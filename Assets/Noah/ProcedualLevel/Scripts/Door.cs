using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Door : MonoBehaviour
{


    //reference to direction 
    [SerializeField] bool x;
    [SerializeField] bool positive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;

            player.transform.position = new Vector2(0, 0);
        }
    }
}
