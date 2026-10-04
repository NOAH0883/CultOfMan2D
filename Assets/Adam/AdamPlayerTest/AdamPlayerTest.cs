using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class AdamPlayerTest : MonoBehaviour
{
    InputAction moveAction;
    InputAction dodgeAction;
    InputAction attackAction;

    // Hurtbox & dodging
    public GameObject playerHurtbox;
    public GameObject playerHurtboxPivot;
    [SerializeField] private float rollDistance;
    [SerializeField] private float rollTime;
    [SerializeField] private float rollInvulnerability;


    // Attacking
    private PlayerHitbox playerHitboxScript;
    public GameObject playerHitbox;
    public GameObject playerHitboxVisuals;
    public BoxCollider2D playerHitboxCollider;
    [SerializeField] private bool testing = false;
    private bool canAttack;
    private bool attacking;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackCooldown;
    int comboCount = 0;
    float comboTimer;

    // Active
    private bool active;
    private float actionTime;

    // Movement & looking
    public GameObject playerVisuals;
    private PlayerVisualsScript playerVisualsScript;
    [SerializeField] TextMeshProUGUI directionText;
    public SpriteRenderer playerSprite;
    float angleDegrees;
    Vector2 playerPos;
    [SerializeField] private float playerSpeed;
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

    

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = true;
        canAttack = true;
        attacking = false;
        moveAction = InputSystem.actions.FindAction("Move");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        attackAction = InputSystem.actions.FindAction("Attack");
        attackAction.performed += ctx => OnLeftClick();
        playerHitboxScript = playerHitbox.GetComponent<PlayerHitbox>();
        playerVisualsScript = playerVisuals.GetComponent<PlayerVisualsScript>();
        playerHitbox.SetActive(false);

        if (rollInvulnerability >= rollTime)
        {
            rollInvulnerability = rollTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            Looking();
            if (angleDegrees >= -90 && angleDegrees <= 90)
            {
                playerSprite.flipX = false;
            }
            else
            {
                playerSprite.flipX = true;
            }
            if (angleDegrees > 0)
            {
                playerVisualsScript.lookingUp = true;
            }
            else
            {
                playerVisualsScript.lookingUp = false;
            }
        }
        
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            comboCount = 0;
        }

        if (active)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            transform.position += new Vector3(moveValue.x, moveValue.y, 0) * playerSpeed * Time.deltaTime;
            if (dodgeAction.WasPressedThisFrame())
            {
                active = false;
                canAttack = false;
                attacking = false;
                StopCoroutine(Attack());
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
            angleDegrees = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            angleDegrees = Mathf.Round(angleDegrees / 45f) * 45f;
            if (testing)
            {
                directionText.text = "Angle: " + angleDegrees + " | Device: " + LookCurrentDevice;
            }
            playerHurtboxPivot.transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
        }
    }
    
    void DodgeRollCalc(Vector2 moveValue)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, 0, 0);

        float angleDegrees = (Mathf.Atan2(moveValue.x, moveValue.y) * Mathf.Rad2Deg);
        if (angleDegrees < 0) { angleDegrees += 360; }
        int angle = Mathf.RoundToInt(angleDegrees / 45f) % 8;
        CurrentDirection = (Direction)angle;

        switch (CurrentDirection)
        {
            case Direction.N: endPos = new Vector3(0, 1, 0); break;
            case Direction.NE: endPos = new Vector3(1, 1, 0); break;
            case Direction.E: endPos = new Vector3(1, 0, 0); break;
            case Direction.SE: endPos = new Vector3(1, -1, 0); break;
            case Direction.S: endPos = new Vector3(0, -1, 0); break;
            case Direction.SW: endPos = new Vector3(-1, -1, 0); break;
            case Direction.W: endPos = new Vector3(-1, 0, 0); break;
            case Direction.NW: endPos = new Vector3(-1, 1, 0); break;
            default: break;
        }
        endPos = startPos + (endPos.normalized * rollDistance);

        Debug.Log("Current Direction Number: " + CurrentDirection);
        StartCoroutine(DodgeRoll(startPos, endPos, rollTime));
        actionTime = rollTime + 0.02f;
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
        canAttack = true;
        Debug.Log("End roll.");
    }

    private void OnLeftClick()
    {
        if (active && canAttack && attackCooldown <= 0 && (GameData.hasWeapon || testing))
        {
            canAttack = false;
            attacking = true;
            Vector2 attackOffset = new Vector2(0, 0);
            Vector2 attackSize = new Vector2(0, 0);
            comboTimer = 0.5f;
            comboCount++;

            if (comboCount >= 3)
            {
                attackOffset = new Vector2(2, 0);
                attackSize = new Vector2 (3, 1);
                playerHitboxScript.damage = 10;
                attackCooldown = 0.3f;
                comboCount = 0;
            } 
            else
            {
                attackOffset = new Vector2(1.5f, 0);
                attackSize = new Vector2(1, 2.5f);
                playerHitboxScript.damage = 5;
                attackCooldown = 0.2f;
            }

            playerHitboxCollider.offset = attackOffset;
            playerHitboxCollider.size = attackSize;
            playerHitboxVisuals.transform.localPosition = attackOffset;
            playerHitboxVisuals.transform.localScale = attackSize;

            StartCoroutine(Attack());
            Debug.Log("Left click.");
          
            
        }
    }

    IEnumerator Attack()
    {
        float elapsed = 0f;

        Debug.Log("Start attack.");
        while (elapsed < attackTime && attacking)
        {
            elapsed += Time.deltaTime;
            playerHitbox.SetActive(true);
            yield return null;
        }
        playerHitbox.SetActive(false);
        Debug.Log("End attack.");
        canAttack = true;
        yield return null;
    }
}
