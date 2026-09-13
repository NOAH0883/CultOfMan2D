using UnityEngine;
using UnityEngine.InputSystem;
using static IInteractable;

public class Player : MonoBehaviour
{


    [SerializeField] float playerSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionDis;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * playerSpeed * Time.deltaTime);
    }


    void OnInteract(InputValue inputValue)
    {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactionDis, interactionLayer);
        if (hit != null)
        {
            if (hit.TryGetComponent<Interactable>(out Interactable interactableObject))
            {

                interactableObject.Interact();
            }
        }
        else
        {
            Debug.Log("No Hit");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionDis);
    }


}

