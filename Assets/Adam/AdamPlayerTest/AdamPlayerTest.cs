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
    public GameObject playerHurtbox;
    public GameObject playerHitbox;
    private bool active;
    private float actionTime;
    [SerializeField] private float rollTime;
    [SerializeField] private float rollInvulnerability;
    Vector3 desiredView;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = true;
        moveAction = InputSystem.actions.FindAction("Move");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        if (rollInvulnerability >= rollTime)
        {
            rollInvulnerability = rollTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            transform.position += new Vector3(moveValue.x, moveValue.y, 0) * playerSpeed * Time.deltaTime;
            if (dodgeAction.IsPressed() && active)
            {
                active = false;
                DodgeRollCalc(moveValue);
                Debug.Log("Dodge roll.");
            }

        }
        else
        {
            actionTime -= Time.deltaTime;
            if (actionTime <= 0)
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

            if (rollX > 0)
            {
                endPos += new Vector3(rollDistance, 0, 0);
                Debug.Log("Right roll.");
            }
            else if (rollX < 0)
            {
                endPos -= new Vector3(rollDistance, 0, 0);
                Debug.Log("Left roll.");
            }
            if (rollY > 0)
            {
                endPos += new Vector3(0, rollDistance, 0);
                Debug.Log("Upwards roll.");
            }
            else if (rollY < 0)
            {
                endPos -= new Vector3(0, rollDistance, 0);
                Debug.Log("Downwards roll.");
            }

            if (endPos == transform.position)
            {
                endPos += new Vector3(rollDistance, 0, 0);
                Debug.Log("Right roll.");
            }

        StartCoroutine(DodgeRoll(startPos, endPos, rollTime));
        actionTime = rollTime + 0.1f;
    }

    IEnumerator DodgeRoll(Vector3 startPos, Vector3 endPos, float rollTime)
    {
        float elapsed = 0f;
        float iFrames = rollInvulnerability;

        Debug.Log("Start roll.");
        while (elapsed < rollTime)
        {
            if (iFrames > 0f)
            {
                playerHurtbox.SetActive(false);
                iFrames -= Time.deltaTime;
            }
            else
            {
                playerHurtbox.SetActive(true);
            }
            elapsed += Time.deltaTime;
            float t = elapsed / rollTime;

            float easeOutT = Mathf.Sin(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(startPos, endPos, easeOutT);
            yield return null;
        }
        transform.position = endPos;
        playerHurtbox.SetActive(true);
        Debug.Log("End roll.");
    }
}
