using UnityEngine;
using UnityEngine.InputSystem;
using static IInteractable;

public class Player : MonoBehaviour
{

    
   
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionDis;

    [SerializeField] PlayerInput input;

    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        
    }



    void OnInteract(InputValue inputValue)
    {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactionDis, interactionLayer);
        if (hit != null)
        {
            if (hit.TryGetComponent<Interactable>(out Interactable interactableObject))
            {
                SoundManager.PlaySound(SoundType.interact);
                
                interactableObject.Interact();
                Debug.Log("interact");

            }
        }
        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionDis);
    }



   
}

