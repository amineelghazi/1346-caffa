using System.Collections;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.5f;
    public float walkDuration = 3f;
    public float idleDuration = 2f;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isWalking = false;
    private Vector2 walkDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start automated wandering loop
        StartCoroutine(PatrolRoutine());
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            // MovePosition handles collision checks during movement instead of clipping through walls
            Vector2 targetPosition = rb.position + (walkDirection * moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // 1. Idle Phase
            isWalking = false;
            UpdateAnimator();
            yield return new WaitForSeconds(idleDuration);

            // 2. Pick Random Direction (Left = -1, Right = 1)
            float randomX = Random.Range(0, 2) == 0 ? -1f : 1f;
            walkDirection = new Vector2(randomX, 0f);

            // Flip sprite based on movement direction
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = walkDirection.x < 0;
            }

            // 3. Walk Phase
            isWalking = true;
            UpdateAnimator();
            yield return new WaitForSeconds(walkDuration);
        }
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool("IsWalking", isWalking);
        }
    }
}