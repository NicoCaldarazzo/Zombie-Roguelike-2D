using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private InputSystem_Actions inputActions;
    [SerializeField] private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   void OnEnable()
   {
        inputActions.Player.Enable();
   }

   void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = inputActions.Player.Move.ReadValue<Vector2>();

        Vector2 targetMovement = new Vector2(moveDirection.x,moveDirection.y);

        rb.MovePosition(rb.position + targetMovement * speed * Time.fixedDeltaTime);

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
