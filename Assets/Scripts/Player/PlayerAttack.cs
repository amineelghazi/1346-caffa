using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform pointDAttaque;       // Drag AttackPoint here
    public float rayonDAttaque = 0.5f;    // Radius of attack circle
    public float degats = 1f;             // Damage per hit
    public LayerMask layerEnnemis;        // Set to "Enemy" layer

    [Header("Input Controls")]
    public KeyCode toucheAttaqueMelee = KeyCode.F;     // Touche F pour le coup au cac
    public KeyCode toucheTir = KeyCode.Mouse0;           // Clic Gauche pour tirer
    public KeyCode toucheEquiperArme = KeyCode.E;       // Touche E pour sortir/ranger l'arme

    [Header("Animation")]
    public Animator animator;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null) return;

        // 1. Sortir ou ranger l'arme (Touche E)
        if (Input.GetKeyDown(toucheEquiperArme))
        {
            ToggleGun();
        }

        bool hasGun = animator.GetBool("HasGun");

        // 2. Attaque corps-à-corps (Touche F) : possible quand l'arme N'EST PAS équipée
        if (Input.GetKeyDown(toucheAttaqueMelee) && !hasGun)
        {
            TriggerMeleeAttack();
        }

        // 3. Tir au pistolet (Clic Gauche) : possible UNIQUEMENT si l'arme EST équipée
        if (Input.GetKeyDown(toucheTir) && hasGun)
        {
            TriggerShootAnimation();
        }
    }

    private void ToggleGun()
    {
        // Inverse la valeur actuelle de HasGun dans l'Animator
        bool currentHasGun = animator.GetBool("HasGun");
        animator.SetBool("HasGun", !currentHasGun);
    }

    private void TriggerMeleeAttack()
    {
        animator.SetTrigger("Attack");
        PerformHitCheck();
    }

    private void TriggerShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    // Call this via an Animation Event on the exact swing frame
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