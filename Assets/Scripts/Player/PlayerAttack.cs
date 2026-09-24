using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform pointDAttaque;       // Drag AttackPoint here
    public float rayonDAttaque = 0.5f;    // Radius of attack circle
    public float degats = 1f;             // Damage per hit
    public LayerMask layerEnnemis;        // Set to "Enemy" layer

    [Header("Input & Animation")]
    [Tooltip("Change key so Space remains strictly for Jumping")]
    public KeyCode toucheAttaque = KeyCode.F; // Use 'F' or KeyCode.Mouse0 for Left-Click
    public Animator animator;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toucheAttaque))
        {
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        // Triggers the transition to the Attack state in Animator
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        
        PerformHitCheck();
    }

    // Call this via an Animation Event on the exact swing frame!
    public void PerformHitCheck()
    {
        if (pointDAttaque == null) return;

        Collider2D[] ennemisTouches = Physics2D.OverlapCircleAll(pointDAttaque.position, rayonDAttaque, layerEnnemis);

        foreach (Collider2D ennemi in ennemisTouches)
        {
            InfectedAnimal sheep = ennemi.GetComponent<InfectedAnimal>();
            if (sheep != null)
            {
                sheep.TakeDamage(degats);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pointDAttaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointDAttaque.position, rayonDAttaque);
    }
}