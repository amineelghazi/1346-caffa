using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMouvement : MonoBehaviour
{
    private static class ParametresAnimateur
    {
        public static readonly int Vitesse = Animator.StringToHash("Speed");        // float
        public static readonly int AuSol = Animator.StringToHash("IsGrounded");     // bool
        public static readonly int VitesseY = Animator.StringToHash("VelocityY");   // float
        public static readonly int Attaque = Animator.StringToHash("Attack");       // trigger
        public static readonly int Degats = Animator.StringToHash("Damage");        // trigger
        public static readonly int Mort = Animator.StringToHash("Death");           // trigger
    }

    private const string AxeHorizontal = "Horizontal";
    private const string BoutonSaut = "Jump";
    private const string BoutonAttaque = "Fire1";
    private const float RatioLargeurDetectionSol = 0.9f;

    [Header("Déplacement")]
    [SerializeField] private float vitesse = 5f;
    [SerializeField] private float forceSaut = 10f;

    [Header("Détection du sol")]
    [SerializeField] private LayerMask coucheSol;
    [SerializeField] private float epaisseurDetectionSol = 0.1f;

    [Header("Sprite")]
    [Tooltip("Coche si le sprite du corbeau regarde vers la droite dans l'image d'origine.")]
    [SerializeField] private bool regardeADroiteParDefaut = true;

    private Rigidbody2D corps;
    private Collider2D collisionneur;
    private Animator animateur;
    private SpriteRenderer rendu;

    private float direction;
    private bool estAuSol;
    private bool sautDemande;
    private bool estMort;

    private void Awake()
    {
        corps = GetComponent<Rigidbody2D>();
        collisionneur = GetComponent<Collider2D>();
        animateur = GetComponentInChildren<Animator>();
        rendu = GetComponentInChildren<SpriteRenderer>();

        VerifierConfiguration();
    }

    private void Update()
    {
        if (estMort) return;

        LireCommandes();
        OrienterSprite();
        MettreAJourAnimations();
    }

    private void FixedUpdate()
    {
        if (estMort) return;

        estAuSol = DetecterSol();
        AppliquerMouvement();
    }

    // À appeler depuis d'autres scripts (ennemis, pièges...)
    public void PrendreDegats()
    {
        if (estMort) return;

        animateur.SetTrigger(ParametresAnimateur.Degats);
    }

    public void Mourir()
    {
        if (estMort) return;

        estMort = true;
        direction = 0f;
        corps.linearVelocity = Vector2.zero;
        animateur.SetTrigger(ParametresAnimateur.Mort);
    }

    private void LireCommandes()
    {
        // Lecture de la direction de déplacement
        direction = Input.GetAxisRaw(AxeHorizontal);

        // Attaque
        if (Input.GetButtonDown(BoutonAttaque))
        {
            animateur.SetTrigger(ParametresAnimateur.Attaque);
        }

        // Saut
        if (Input.GetButtonDown(BoutonSaut))
        {
            sautDemande = true;
        }
    }

    private void OrienterSprite()
    {
        if (direction == 0f) return;

        bool versGauche = direction < 0f;
        rendu.flipX = regardeADroiteParDefaut ? versGauche : !versGauche;
    }

    private void AppliquerMouvement()
    {
        Vector2 vitesseActuelle = corps.linearVelocity;
        vitesseActuelle.x = direction * vitesse;

        if (sautDemande && estAuSol)
        {
            vitesseActuelle.y = forceSaut;
        }

        sautDemande = false;
        corps.linearVelocity = vitesseActuelle;
    }

    private bool DetecterSol()
    {
        Bounds zone = ObtenirZoneDetectionSol();
        return Physics2D.OverlapBox(zone.center, zone.size, 0f, coucheSol) != null;
    }

    private Bounds ObtenirZoneDetectionSol()
    {
        Bounds limites = collisionneur.bounds;
        Vector2 centre = new Vector2(limites.center.x, limites.min.y - epaisseurDetectionSol / 2f);
        Vector2 taille = new Vector2(limites.size.x * RatioLargeurDetectionSol, epaisseurDetectionSol);
        return new Bounds(centre, taille);
    }

    private void MettreAJourAnimations()
    {
        animateur.SetFloat(ParametresAnimateur.Vitesse, Mathf.Abs(direction));
        animateur.SetBool(ParametresAnimateur.AuSol, estAuSol);
        animateur.SetFloat(ParametresAnimateur.VitesseY, corps.linearVelocity.y);
    }

    private void VerifierConfiguration()
    {
        if (coucheSol.value == 0)
            Debug.LogWarning("PlayerMouvement : 'Couche Sol' est sur Nothing, le corbeau ne pourra jamais sauter.", this);

        if (animateur == null)
            Debug.LogError("PlayerMouvement : aucun Animator trouvé sur le corbeau ou ses enfants.", this);

        if (rendu == null)
            Debug.LogError("PlayerMouvement : aucun SpriteRenderer trouvé sur le corbeau ou ses enfants.", this);
    }

    private void OnDrawGizmosSelected()
    {
        if (collisionneur == null)
            collisionneur = GetComponent<Collider2D>();

        if (collisionneur == null) return;

        Bounds zone = ObtenirZoneDetectionSol();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(zone.center, zone.size);
    }
}