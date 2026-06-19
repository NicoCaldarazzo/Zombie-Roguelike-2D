using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private UpgradeStats upgradeStats;
    private float finalSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        upgradeStats = GetComponent<UpgradeStats>();
        finalSpeed = speed;
    }

    // Update is called once per frame
   void OnEnable()
   {
        inputActions.Player.Enable();
        GameEvents.OnUpgradeApplied += RecalculateStats;
   }

   void OnDisable()
    {
        inputActions.Player.Disable();
        GameEvents.OnUpgradeApplied -= RecalculateStats;
    }

    private void RecalculateStats()
    {
        finalSpeed = speed * (1+ upgradeStats.GetModifier(UpgradeType.PlayerSpeed) / 100f);
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = inputActions.Player.Move.ReadValue<Vector2>();

        Vector2 targetMovement = new Vector2(moveDirection.x,moveDirection.y);

        rb.MovePosition(rb.position + targetMovement * finalSpeed * Time.fixedDeltaTime);

        if (moveDirection.x <0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
