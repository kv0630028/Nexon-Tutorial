using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private PlayerInputActions inputActions;
    private Camera mainCamera;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        AimFirePoint();

        if (inputActions.Player.Attack.WasPressedThisFrame())
        {
            Debug.Log("Attack");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void AimFirePoint()
    {
        Vector3 screenPosition = Mouse.current.position.ReadValue();
        screenPosition.z = Mathf.Abs(mainCamera.transform.position.z);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        Vector2 direction = mouseWorldPosition - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
