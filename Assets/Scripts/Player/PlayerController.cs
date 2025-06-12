using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Interact Settings")]
    public float interactRadius = 10;
    public Transform interactStart;

    [Header("Planting")]
    public bool unlockedPlanting = true;
    public GameObject plantPrefab;
    public float plantDistance = 1f;
    public float groundRayLength = 3f;
    public float plantCheckRadius = 0.2f;
    private GameObject lastPlant;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask[] groundLayer;

    [Header("Collectable Text")]
    public TextMeshProUGUI collectableText;


    [Header("Inventory")]
    public List<string> collectedIDs = new List<string>();

    [HideInInspector] public float speedMultiplier = 1f;
    [HideInInspector] public int currentCollectables = 0;
    [HideInInspector] public Checkpoint lastCheckPoint;

    private Rigidbody2D rb;
    private Animator anim;
    private float lastGroundedTime;
    private int jumpCount;
    private bool canCoyote = true;
    private Vector2 moveInput;
    private Vector2 lastCheckpoint;
    private bool canMove = true;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        SetCheckpoint(this.gameObject.transform.position);

    }

    void FixedUpdate()
    {
        if (isGrounded())
        {
            lastGroundedTime = Time.time;
            jumpCount = maxJumps;
        }

        anim.SetFloat("magnitude", rb.linearVelocity.magnitude);
        anim.SetBool("isGrounded", isGrounded());


        float speed = moveSpeed * speedMultiplier;
        if(canMove) rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        if(!canMove) rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        canCoyote = Time.time - lastGroundedTime <= coyoteTime;

        collectableText.SetText(currentCollectables.ToString());

        HandleSpriteFlip();
    }

    public bool isGrounded()
    {
        bool grounded = false;
        foreach(LayerMask layer in groundLayer)
        {
            if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layer))
            {
                grounded = true;
            }
        }
        return grounded;
    }

    private void HandleSpriteFlip()
    {
        if (moveInput.x > 0.01f)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else if (moveInput.x < -0.01f)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }

    public void SwitchMove(bool newState)
    {
        canMove = newState;
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
        if (context.started && canMove)
        {
            if (jumpCount > 0 && (isGrounded() || canCoyote))
            {
                anim.SetTrigger("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }else if(jumpCount > 1)
            {
                anim.SetTrigger("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount--;
            }
        }
    }


    public void InteractInput(InputAction.CallbackContext context)
    {
        if (context.started && canMove)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(interactStart.position, interactRadius);
            foreach (Collider2D col in colliders)
            {
                Interactable interactable = col.gameObject.GetComponent<Interactable>();
                if(interactable != null)
                {
                    anim.SetTrigger("plantFlower");
                    //interactable.Interact(this);
                    Debug.Log("interacted");
                }
            }
        }
    }

    public void PlantInput(InputAction.CallbackContext context)
    {
        if (context.started && canMove && plantPrefab != null && unlockedPlanting)
        {
            anim.SetTrigger("plantFlower");
            Vector3 facingDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;

            Vector3 spawnCheckOrigin = transform.position + facingDirection * plantDistance + Vector3.up * 0.5f;
            Debug.DrawLine(spawnCheckOrigin, spawnCheckOrigin + Vector3.down * groundRayLength, Color.red, 1f);
            RaycastHit2D hit = Physics2D.CircleCast(spawnCheckOrigin, groundCheckRadius, Vector2.down, groundRayLength, groundLayer[0]);

            if (hit.collider != null)
            {
                Vector3 plantPos = new Vector3(
                    Mathf.Round(hit.point.x),
                    Mathf.Round(hit.point.y + 0.5f),
                    0f
                );

                Collider2D existing = Physics2D.OverlapCircle(plantPos, plantCheckRadius);
                if (existing == null)
                {
                    if(lastPlant != null) Destroy(lastPlant);
                    lastPlant = Instantiate(plantPrefab, plantPos, Quaternion.identity);
                }
            }
        }
    }




    /////////////////////////

    /////////////////////////
    ///    Checkpoint     /// 
    /////////////////////////

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }

    public void ResetPlayer()
    {
        this.gameObject.transform.position = lastCheckpoint;
    }

    Coroutine loadPointC;

    private IEnumerator loadPoint()
    {
        yield return new WaitForSeconds(1.5f);
        rb.gravityScale = 1;
        ResetPlayer();
        yield return new WaitForSeconds(.4f);
        FadeManager.instance.FadeIn();
        yield return new WaitForSeconds(.8f);
        canMove = true;

    }

    /////////////////////////

    /////////////////////////
    ///    Collisiom      /// 
    /////////////////////////

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "oil") speedMultiplier = 0.4f;

        if(collision.tag == "Deathzone")
        {
            rb.gravityScale = 0; 
            rb.angularVelocity = 0; 
            rb.linearVelocityY = 0; 
            rb.linearVelocityX = 0; 
            canMove = false;
            FadeManager.instance.FadeOut();
            if(loadPointC != null) StopCoroutine(loadPointC);

            loadPointC = StartCoroutine(loadPoint());
        }

        if (collision.tag == "Checkpoint")
        {
            SetCheckpoint(this.gameObject.transform.position);
        }
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
            Gizmos.color = Color.aliceBlue;
            Gizmos.DrawWireSphere(interactStart.position, interactRadius);


            Vector3 facingDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            Vector3 spawnCheckOrigin = transform.position + facingDirection * plantDistance;

        }
    }

    /////////////////////////

}
