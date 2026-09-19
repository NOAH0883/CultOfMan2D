using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AdamPlayerTest : MonoBehaviour
{
    InputAction moveAction;
    InputAction dodgeAction;
    InputAction lookAction;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rollDistance;
    public GameObject playerHurtbox;
    public GameObject playerHurtboxPivot;
    public GameObject playerHitbox;
    private bool active;
    private float actionTime;
    
    [SerializeField] TextMeshProUGUI directionText;
    Vector2 playerPos;
    Vector2 lookInput = new Vector2 (0, 0);
    enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }   
    Direction CurrentDirection;
    enum PlayerLookDevice { Mouse, Joystick, Gamepad }
    PlayerLookDevice LookCurrentDevice;

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
        Looking();

        if (active)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            
            transform.position += new Vector3(moveValue.x, moveValue.y, 0) * playerSpeed * Time.deltaTime;
            if (dodgeAction.WasPressedThisFrame() && active)
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

    void Looking()
    {
        Vector2 relativePos = Vector2.zero;
        playerPos = transform.position;

        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            if (mouseDelta.sqrMagnitude > 0.01f)
            {
                LookCurrentDevice = PlayerLookDevice.Mouse;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            relativePos = mouseWorldPos - playerPos;
        }
        if (Joystick.current != null)
        {
            AxisControl z = Joystick.current["z"] as AxisControl;
            AxisControl rz = Joystick.current["rz"] as AxisControl;

            if (z != null && rz != null)
            {
                Vector2 joystickInput = new Vector2(z.ReadValue(), -rz.ReadValue());

                if (joystickInput.sqrMagnitude > 0.01f)
                {
                    LookCurrentDevice = PlayerLookDevice.Joystick;
                    lookInput = joystickInput;
                }
            }
        }
        if (Gamepad.current != null)
        {
            Vector2 gamepadInput = Gamepad.current.rightStick.ReadValue();

            if (gamepadInput.sqrMagnitude > 0.01f)
            {
                LookCurrentDevice = PlayerLookDevice.Gamepad;
                lookInput = gamepadInput;
            }
        }

        switch (LookCurrentDevice)
        {
            case PlayerLookDevice.Joystick:
                relativePos = lookInput;
                break;

            case PlayerLookDevice.Gamepad:
                relativePos = lookInput;
                break;

            case PlayerLookDevice.Mouse:
                Vector2 mousePosition =
                    Mouse.current.position.ReadValue();

                Vector2 mouseWorldPos =
                    Camera.main.ScreenToWorldPoint(mousePosition);

                relativePos = mouseWorldPos - playerPos;
                break;
        }


        if (relativePos.sqrMagnitude > 0.01f)
        {
            float angleDegrees = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            angleDegrees = Mathf.Round(angleDegrees / 45f) * 45f;
            directionText.text = "Angle: " + angleDegrees + " | Device: " + LookCurrentDevice;
            playerHurtboxPivot.transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
        }
    }
    
    void DodgeRollCalc(Vector2 moveValue)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;

        float angleDegrees = (Mathf.Atan2(moveValue.x, moveValue.y) * Mathf.Rad2Deg);
        if (angleDegrees < 0) { angleDegrees += 360; }
        int angle = Mathf.RoundToInt(angleDegrees / 45f) % 8;
        CurrentDirection = (Direction)angle;

        switch (CurrentDirection)
        {
            case Direction.N: endPos += new Vector3(0, rollDistance, 0); break;
            case Direction.NE: endPos += new Vector3(rollDistance, rollDistance, 0); break;
            case Direction.E: endPos += new Vector3(rollDistance, 0, 0); break;
            case Direction.SE: endPos += new Vector3(rollDistance, -rollDistance, 0); break;
            case Direction.S: endPos += new Vector3(0, -rollDistance, 0); break;
            case Direction.SW: endPos += new Vector3(-rollDistance, -rollDistance, 0); break;
            case Direction.W: endPos += new Vector3(-rollDistance, 0, 0); break;
            case Direction.NW: endPos += new Vector3(-rollDistance, rollDistance, 0); break;
            default: break;
        }

        Debug.Log("Current Direction Number: " + CurrentDirection);
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
