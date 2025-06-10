using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 14f;
    public float coyoteTime = 0.2f;
    public int maxJumps = 2;
   

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [HideInInspector] public float speedMultiplier = 1f;

    private Rigidbody2D rb;
    private float lastGroundedTime;
    private int jumpCount;
    private bool canCoyote = true;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded())
        {
            lastGroundedTime = Time.time;
            jumpCount = maxJumps;
        }


        float speed = moveSpeed * speedMultiplier;
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);

        canCoyote = Time.time - lastGroundedTime <= coyoteTime;
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    /////////////////////////

    /////////////////////////
    ///      Inputs       /// 
    /////////////////////////

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (jumpCount > 0 && (isGrounded() || canCoyote))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }else if(jumpCount > 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }
        }
    }

    /////////////////////////

    /////////////////////////
    ///    Collisiom      /// 
    /////////////////////////

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "oil") speedMultiplier = 0.4f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "oil") speedMultiplier = 1f;
    }

    /////////////////////////

    /////////////////////////
    ///      Gizmo        /// 
    /////////////////////////

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    /////////////////////////

}
