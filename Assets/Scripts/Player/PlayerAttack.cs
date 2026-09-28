using System.Collections.Generic; // NEW
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform pointDAttaque;       // Drag AttackPoint here
    public float rayonDAttaque = 0.5f;    // Radius of attack circle
    public float degats = 1f;             // Damage per hit
    public LayerMask layerEnnemis;        // Set to "Enemy" layer

    // NEW
    [Header("Attack Timing")]
    [Tooltip("Temps minimum entre deux coups au corps à corps.")]
    public float delaiEntreAttaques = 0.5f;
    [Tooltip("Délai entre l'appui sur la touche et le moment où le coup touche (0 = immédiat).")]
    public float delaiCoup = 0f;
    [Tooltip("Coche si tu appelles PerformHitCheck depuis un Animation Event. Le coup ne sera alors PAS déclenché à l'appui sur la touche (évite de toucher deux fois).")]
    public bool frappeParEvenementAnimation = false;
    [Tooltip("Place automatiquement le point d'attaque du bon côté quand le sprite se retourne (flipX).")]
    public bool orienterPointAutomatiquement = true;

    [Header("Input Controls")]
    public KeyCode toucheAttaqueMelee = KeyCode.F;     // Touche F pour le coup au cac
    public KeyCode toucheTir = KeyCode.Mouse0;           // Clic Gauche pour tirer
    public KeyCode toucheEquiperArme = KeyCode.E;       // Touche E pour sortir/ranger l'arme

    [Header("Animation")]
    public Animator animator;

    // NEW
    private SpriteRenderer rendu;
    private float decalageXDepart;
    private bool flipDepart;
    private float prochaineAttaque;

    private void Start()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>(); // NEW : cherche aussi dans les enfants

        // NEW : mémorise la position de départ du point d'attaque pour pouvoir le retourner
        rendu = GetComponentInChildren<SpriteRenderer>();
        if (pointDAttaque != null) decalageXDepart = pointDAttaque.localPosition.x;
        if (rendu != null) flipDepart = rendu.flipX;
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
        if (Input.GetKeyDown(toucheAttaqueMelee) && !hasGun && Time.time >= prochaineAttaque) // NEW : cooldown
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
        prochaineAttaque = Time.time + delaiEntreAttaques; // NEW
        animator.SetTrigger("Attack");

        // NEW : soit coup immédiat, soit avec délai, soit uniquement via Animation Event
        if (frappeParEvenementAnimation) return;

        if (delaiCoup > 0f) Invoke(nameof(PerformHitCheck), delaiCoup);
        else PerformHitCheck();
    }

    private void TriggerShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    // Call this via an Animation Event on the exact swing frame
    public void PerformHitCheck()
    {
        if (pointDAttaque == null) return;

        OrienterPointDAttaque(); // NEW

        Collider2D[] ennemisTouches = Physics2D.OverlapCircleAll(pointDAttaque.position, rayonDAttaque, layerEnnemis);
        HashSet<Component> dejaTouches = new HashSet<Component>(); // NEW : évite de toucher 2x le même ennemi

        foreach (Collider2D ennemi in ennemisTouches)
        {
            // NEW : GetComponentInParent fonctionne même si le collider est sur un enfant
            InfectedAnimal sheep = ennemi.GetComponentInParent<InfectedAnimal>();
            if (sheep != null)
            {
                if (dejaTouches.Add(sheep)) sheep.TakeDamage(degats);
                continue;
            }

            // NEW : la hyène
            HyeneHealth hyene = ennemi.GetComponentInParent<HyeneHealth>();
            if (hyene != null && dejaTouches.Add(hyene))
            {
                hyene.PrendreDegats(degats);
            }
        }
    }

    // NEW : le flipX du sprite ne retourne pas les objets enfants, donc on retourne le point à la main
    private void OrienterPointDAttaque()
    {
        if (!orienterPointAutomatiquement || pointDAttaque == null || rendu == null) return;

        Vector3 p = pointDAttaque.localPosition;
        p.x = (rendu.flipX != flipDepart) ? -decalageXDepart : decalageXDepart;
        pointDAttaque.localPosition = p;
    }

    private void OnDrawGizmosSelected()
    {
        if (pointDAttaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointDAttaque.position, rayonDAttaque);
    }
}