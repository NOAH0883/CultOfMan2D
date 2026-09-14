using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdamPlayerTest : MonoBehaviour
{
    InputAction moveAction;
    InputAction dodgeAction;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rollDistance;
    private bool active;
    private float idleTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = true;
        moveAction = InputSystem.actions.FindAction("Move");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            transform.position += new Vector3(moveValue.x, moveValue.y, 0) * playerSpeed * Time.deltaTime;
            if (moveAction.IsPressed())
            {
                Debug.Log(moveAction);
            }

            if (dodgeAction.IsPressed() && active)
            {
                active = false;
                DodgeRollCalc(moveValue);
                Debug.Log("Dodge roll.");
            }
        }
        else
        {
            idleTime -= Time.deltaTime;
            if (idleTime <= 0)
            {
                active = true;
            }
        }
        
    }

    void DodgeRollCalc(Vector2 moveValue)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        float rollX = moveValue.x;
        float rollY = moveValue.y;
        if (rollX != 0 || rollY == 0)
        {
            if (rollX >= 0)
            {
                endPos = transform.position + new Vector3(rollDistance, 0, 0);
                Debug.Log("Right roll.");
            }
            else if (rollX < 0)
            {
                endPos = transform.position - new Vector3(rollDistance, 0, 0);
                Debug.Log("Left roll.");
            }
        }
        else
        {
            if (rollY > 0)
            {
                endPos = transform.position + new Vector3(0, rollDistance, 0);
                Debug.Log("Upwards roll.");
            }
            else if (rollY < 0)
            {
                endPos = transform.position - new Vector3(0, rollDistance, 0);
                Debug.Log("Downwards roll.");
            }
        }

        idleTime = 0.3f;
        StartCoroutine(DodgeRoll(startPos, endPos, idleTime));
        transform.position = endPos;
    }

    IEnumerator DodgeRoll(Vector3 startPos, Vector3 endPos, float idleTime)
    {
        float elapsed = 0f;
        print("Start roll.");
        while (elapsed < idleTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / idleTime;

            float easeOutT = Mathf.Sin(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(startPos, endPos, easeOutT);
            yield return null;
        }
        transform.position = endPos;
        print("End roll.");
    }
}
