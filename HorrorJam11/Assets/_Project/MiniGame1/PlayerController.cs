using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform target;
    public float radius;
    public float correctionSpeed;
    public float sideSpeed;
    public float dampSpeed;

    private InputActions_minigames inputActions;
    private float moveInputValue;
    private bool startDamp;

    private void OnEnable()
    {
        inputActions = new InputActions_minigames();
        inputActions.Disable();
        inputActions.Minigame_circle.Enable();

        inputActions.Minigame_circle.Move.performed += MovePlayer;
        inputActions.Minigame_circle.Move.canceled += TurnDampOn;
    }

    private void OnDisable()
    {
        inputActions.Minigame_circle.Move.performed -= MovePlayer;
        inputActions.Minigame_circle.Move.canceled -= TurnDampOn;


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.up = target.transform.position - transform.position;
        float currentDistance = (target.transform.position - transform.position).magnitude;
        if (currentDistance != radius)
        {
            transform.Translate(0, (currentDistance - radius) * correctionSpeed * Time.fixedDeltaTime, 0, Space.Self);
        }
        transform.Translate(moveInputValue * sideSpeed * Time.fixedDeltaTime, 0, 0, Space.Self);

        DampMovement(startDamp);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        moveInputValue = context.ReadValue<float>();
        startDamp = false;
    }

    public void DampMovement(bool StarDamp)
    {
        if (StarDamp)
        {
            if (moveInputValue < 0)
            {
                moveInputValue = Mathf.Min(0, moveInputValue + dampSpeed * Time.fixedDeltaTime);
            }
            else if (moveInputValue > 0)
            {
                moveInputValue = Mathf.Max(0, moveInputValue - dampSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void TurnDampOn(InputAction.CallbackContext context)
    {
        startDamp = true;
    }
}
