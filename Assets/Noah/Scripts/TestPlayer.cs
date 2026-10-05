using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * playerSpeed * Time.deltaTime);
    }

}
