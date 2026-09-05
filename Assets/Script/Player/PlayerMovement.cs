using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D playerRb;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        playerRb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Debug.Log($"moveInput: {moveInput}");
    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}