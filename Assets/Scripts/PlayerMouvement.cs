using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMouvement : MonoBehaviour
{
    [Header("Déplacement")]
    [SerializeField] private float vitesse = 5f;
    [SerializeField] private float forceSaut = 10f;

    [Header("Détection du sol")]
    [SerializeField] private Transform verificationSol;   // enfant vide placé aux pieds
    [SerializeField] private float rayonSol = 0.15f;
    [SerializeField] private LayerMask coucheSol;

    [Header("Sprite")]
    [Tooltip("Coche si le sprite du corbeau regarde vers la droite dans l'image d'origine.")]
    [SerializeField] private bool regardeADroiteParDefaut = true;

    private Rigidbody2D corps;
    private Animator animateur;
    private SpriteRenderer rendu;

    private float horizontal;
    private bool auSol;
    private bool sautDemande;
    private bool mort;

    // Noms des paramètres de l'Animator (à créer dans la fenêtre Animator > Parameters)
    private static readonly int ParamVitesse = Animator.StringToHash("Speed");      // float
    private static readonly int ParamAuSol = Animator.StringToHash("IsGrounded"); // bool
    private static readonly int ParamVitesseY = Animator.StringToHash("VelocityY");  // float
    private static readonly int ParamAttaque = Animator.StringToHash("Attack");     // trigger
    private static readonly int ParamDegats = Animator.StringToHash("Damage");     // trigger
    private static readonly int ParamMort = Animator.StringToHash("Death");      // trigger

    private void Awake()
    {
        corps = GetComponent<Rigidbody2D>();
        animateur = GetComponentInChildren<Animator>();
        rendu = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (mort)
        {
            horizontal = 0f;
            return;
        }

        // Lecture des commandes (horizontal seulement : c'est un plateformer)
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && auSol)
            sautDemande = true;

        if (Input.GetButtonDown("Fire1"))
            animateur.SetTrigger(ParamAttaque);

        // Retourner le sprite selon la direction
        if (horizontal != 0f)
        {
            bool versGauche = horizontal < 0f;
            rendu.flipX = regardeADroiteParDefaut ? versGauche : !versGauche;
        }

        MettreAJourAnimations();
    }

    private void FixedUpdate()
    {
        Vector2 position = verificationSol != null ? (Vector2)verificationSol.position : corps.position;
        auSol = Physics2D.OverlapCircle(position, rayonSol, coucheSol);

        // On modifie la vitesse au lieu d'utiliser MovePosition : la gravité continue de s'appliquer.
        // (Unity 6 : linearVelocity. Sur les anciennes versions, remplace par velocity.)
        Vector2 v = corps.linearVelocity;
        v.x = horizontal * vitesse;

        if (sautDemande && auSol)
            v.y = forceSaut;

        sautDemande = false;
        corps.linearVelocity = v;
    }

    private void MettreAJourAnimations()
    {
        animateur.SetFloat(ParamVitesse, Mathf.Abs(horizontal));
        animateur.SetBool(ParamAuSol, auSol);
        animateur.SetFloat(ParamVitesseY, corps.linearVelocity.y);
    }

    // À appeler depuis d'autres scripts (ennemis, pièges...)
    public void PrendreDegats()
    {
        if (mort) return;
        animateur.SetTrigger(ParamDegats);
    }

    public void Mourir()
    {
        if (mort) return;
        mort = true;
        corps.linearVelocity = Vector2.zero;
        animateur.SetTrigger(ParamMort);
    }

    private void OnDrawGizmosSelected()
    {
        if (verificationSol == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(verificationSol.position, rayonSol);
    }
}