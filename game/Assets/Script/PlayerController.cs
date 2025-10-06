using UnityEngine;
using UnityEngine.InputSystem;  // Input System mới

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprint_speed = 8f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public static bool sprint_check;
    public static bool walk_check;
    public float decreaseAmount;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private PlayerInputActions inputActions;   // Input System actions
    private InputAction moveAction;
    private InputAction sprintAction;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        // ✅ Tự động lấy Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Nếu có HealthBar trong scene thì tự động tìm
        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }

        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        moveAction = inputActions.Player.Move;      // "Player" là tên action map
        sprintAction = inputActions.Player.Sprint;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        InputMovement();
    }

    void FixedUpdate()
    {
        Move();
        Sprint();
    }

    void InputMovement()
    {
        moveDirection = moveAction.ReadValue<Vector2>();
    }

    void Move()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    void Sprint()
    {
        Stamina stamina = FindObjectOfType<Stamina>();
        bool isMoving = moveDirection.sqrMagnitude > 0.01f;

        if (sprintAction.IsPressed() && isMoving && stamina != null && stamina.currentStamina > 0)
        {
            sprint_check = true;
            walk_check = false;
            speed = sprint_speed;
        }
        else
        {
            speed = 5f;
            sprint_check = false;
            walk_check = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Reload scene hoặc game over UI
    }
}
