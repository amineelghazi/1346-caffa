using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class HyeneIA : MonoBehaviour
{
    private static class ParametresAnimateur
    {
        public static readonly int Vitesse = Animator.StringToHash("Speed");    // float
        public static readonly int Attaque = Animator.StringToHash("Attack");   // trigger
    }

    private enum Etat { Patrouille, Poursuite, Attaque }

    [Header("Déplacement")]
    [SerializeField] private float vitessePatrouille = 1.5f;
    [SerializeField] private float vitessePoursuite = 3.5f;

    [Header("Patrouille")]
    [Tooltip("Distance max que la hyène parcourt de chaque côté de son point de départ.")]
    [SerializeField] private float distancePatrouille = 4f;

    [Header("Détection du joueur")]
    [SerializeField] private float rayonDetection = 6f;
    [Tooltip("Si le joueur s'éloigne au-delà de cette distance, la hyène abandonne la poursuite.")]
    [SerializeField] private float rayonAbandon = 9f;

    [Header("Attaque")]
    [Tooltip("Points de vie retirés au joueur à chaque morsure.")]
    [SerializeField] private float degats = 10f;
    [SerializeField] private float porteeAttaque = 1.2f;
    [SerializeField] private float dureeAttaque = 0.6f;
    [Tooltip("Délai entre le début de l'animation et le moment où le coup touche.")]
    [SerializeField] private float delaiDegats = 0.25f;
    [SerializeField] private float delaiEntreAttaques = 1.5f;

    [Header("Détection des obstacles")]
    [Tooltip("Couche du sol et des murs. NE PAS inclure la couche de la hyène elle-même.")]
    [SerializeField] private LayerMask coucheSol;

    [Header("Sprite")]
    [SerializeField] private bool regardeADroiteParDefaut = true;

    private Rigidbody2D corps;
    private Collider2D collisionneur;
    private Animator animateur;
    private SpriteRenderer rendu;

    private Transform joueur;
    private PlayerHealth santeJoueur;

    private Etat etat = Etat.Patrouille;
    private float direction = 1f;
    private float positionDepartX;
    private float finAttaque;
    private float momentDegats;
    private float prochaineAttaque;
    private bool degatsInfliges;

    private void Awake()
    {
        corps = GetComponent<Rigidbody2D>();
        collisionneur = GetComponent<Collider2D>();
        animateur = GetComponentInChildren<Animator>();
        rendu = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        positionDepartX = transform.position.x;

        GameObject objetJoueur = GameObject.FindGameObjectWithTag("Player");
        if (objetJoueur != null)
        {
            joueur = objetJoueur.transform;
            santeJoueur = objetJoueur.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogWarning("HyeneIA : aucun objet avec le tag 'Player' trouvé.", this);
        }

        if (coucheSol.value == 0)
            Debug.LogWarning("HyeneIA : 'Couche Sol' est sur Nothing, la hyène ne détectera ni murs ni bords.", this);
    }

    private void FixedUpdate()
    {
        switch (etat)
        {
            case Etat.Patrouille: Patrouiller(); break;
            case Etat.Poursuite: Poursuivre(); break;
            case Etat.Attaque: Attaquer(); break;
        }

        MettreAJourSprite();
        animateur.SetFloat(ParametresAnimateur.Vitesse, Mathf.Abs(corps.linearVelocity.x));
    }

    // ---------- États ----------

    private void Patrouiller()
    {
        if (JoueurDansRayon(rayonDetection))
        {
            etat = Etat.Poursuite;
            return;
        }

        // Demi-tour si on dépasse la zone de patrouille, ou si mur / vide devant
        float ecart = transform.position.x - positionDepartX;
        bool tropLoin = (direction > 0f && ecart >= distancePatrouille) ||
                        (direction < 0f && ecart <= -distancePatrouille);

        if (tropLoin || MurDevant() || VideDevant())
        {
            direction = -direction;
        }

        Deplacer(direction * vitessePatrouille);
    }

    private void Poursuivre()
    {
        if (joueur == null || !JoueurDansRayon(rayonAbandon))
        {
            etat = Etat.Patrouille;
            return;
        }

        float ecartX = joueur.position.x - transform.position.x;
        direction = ecartX >= 0f ? 1f : -1f;

        // Assez près (et attaque disponible) : on attaque
        if (Mathf.Abs(ecartX) <= porteeAttaque && Time.time >= prochaineAttaque)
        {
            CommencerAttaque();
            return;
        }

        // Assez près mais en cooldown : on reste face au joueur sans avancer
        if (Mathf.Abs(ecartX) <= porteeAttaque || MurDevant() || VideDevant())
        {
            Deplacer(0f);
            return;
        }

        Deplacer(direction * vitessePoursuite);
    }

    private void Attaquer()
    {
        Deplacer(0f);

        // Le coup touche à mi-animation, si le joueur est toujours à portée
        if (!degatsInfliges && Time.time >= momentDegats)
        {
            degatsInfliges = true;

            if (joueur != null && santeJoueur != null &&
                Vector2.Distance(transform.position, joueur.position) <= porteeAttaque * 1.3f)
            {
                santeJoueur.PrendreDegats(degats);
            }
        }

        if (Time.time >= finAttaque)
        {
            etat = Etat.Poursuite;
        }
    }

    private void CommencerAttaque()
    {
        etat = Etat.Attaque;
        degatsInfliges = false;
        momentDegats = Time.time + delaiDegats;
        finAttaque = Time.time + dureeAttaque;
        prochaineAttaque = Time.time + delaiEntreAttaques;

        Deplacer(0f);
        animateur.SetTrigger(ParametresAnimateur.Attaque);
    }

    // ---------- Outils ----------

    private void Deplacer(float vitesseX)
    {
        Vector2 v = corps.linearVelocity;
        v.x = vitesseX;
        corps.linearVelocity = v;
    }

    private bool JoueurDansRayon(float rayon)
    {
        if (joueur == null) return false;
        return Vector2.Distance(transform.position, joueur.position) <= rayon;
    }

    private bool MurDevant()
    {
        Bounds b = collisionneur.bounds;
        Vector2 origine = new Vector2(b.center.x, b.center.y);
        float longueur = b.extents.x + 0.15f;
        return Physics2D.Raycast(origine, Vector2.right * direction, longueur, coucheSol);
    }

    private bool VideDevant()
    {
        Bounds b = collisionneur.bounds;
        Vector2 origine = new Vector2(b.center.x + direction * (b.extents.x + 0.1f), b.min.y + 0.1f);
        return !Physics2D.Raycast(origine, Vector2.down, 0.6f, coucheSol);
    }

    private void MettreAJourSprite()
    {
        bool versGauche = direction < 0f;
        rendu.flipX = regardeADroiteParDefaut ? versGauche : !versGauche;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rayonDetection);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, porteeAttaque);

        Gizmos.color = Color.cyan;
        float centre = Application.isPlaying ? positionDepartX : transform.position.x;
        Gizmos.DrawLine(new Vector3(centre - distancePatrouille, transform.position.y, 0f),
                        new Vector3(centre + distancePatrouille, transform.position.y, 0f));
    }
}