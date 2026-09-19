using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static IInteractable;

public class Player : MonoBehaviour
{


    [SerializeField] float playerSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionDis;

    [SerializeField] VillageManager villageManager;
    public int food;
    PlayerInput input;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
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

                interactableObject.Interact(villageManager);

            }
        }
        else
        {
            return;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionDis);
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        rb = GetComponent<Rigidbody2D>();
        if (input != null)
        {
            // 4. Force reset the input system to refresh device bindings
            input.enabled = false;
            input.enabled = true;

        }
    }
}

