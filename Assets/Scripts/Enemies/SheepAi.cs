using System.Collections;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.5f;
    public float walkDuration = 3f;
    public float idleDuration = 2f;

    [Header("Wall Detection")]
    public Transform wallCheck;          // Empty GameObject positioned at the front of the sheep
    public float wallCheckRadius = 0.1f;
    public LayerMask wallLayer;          // Set to your Ground/Wall layer in the Inspector

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isWalking = false;
    private float moveDirection = 1f;    // 1 = Right, -1 = Left
    private Coroutine patrolCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    private void FixedUpdate()
    {
        // 1. Check for wall collision while walking
        if (isWalking && IsHittingWall())
        {
            HandleWallHit();
            return;
        }

        // 2. Standard horizontal movement preserving gravity
        if (isWalking)
        {
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    private bool IsHittingWall()
    {
        if (wallCheck == null) return false;

        // Checks if the wallCheck point overlaps with any collider on the wallLayer
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    private void HandleWallHit()
    {
        // Stop horizontal velocity immediately
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        // Turn off walking state and update animation
        isWalking = false;
        UpdateAnimator();

        // Turn around
        moveDirection *= -1f;
        UpdateSpriteFlip();

        // Small nudge to push the sheep out of the wall/corner collider
        transform.position += new Vector3(moveDirection * 0.1f, 0f, 0f);

        // Restart patrol loop into Idle
        if (patrolCoroutine != null)
        {
            StopCoroutine(patrolCoroutine);
        }
        patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // --- IDLE STATE ---
            isWalking = false;
            UpdateAnimator();
            yield return new WaitForSeconds(idleDuration);

            // --- WALK STATE ---
            isWalking = true;
            UpdateAnimator();
            yield return new WaitForSeconds(walkDuration);
        }
    }

    private void UpdateSpriteFlip()
    {
        // 1. Flip the entire GameObject scale on X
        Vector3 localScale = transform.localScale;

        // Assumes original prefab artwork faces RIGHT by default
        if (moveDirection > 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        else if (moveDirection < 0)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }

        transform.localScale = localScale;

        // 2. Make sure spriteRenderer.flipX is UNCHECKED so they don't double-flip
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visual indicator in Scene View for the wall check radius
        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
    }
}