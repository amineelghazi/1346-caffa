using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HyeneHealth : MonoBehaviour
{
    private static readonly int ParamMort = Animator.StringToHash("Death");   
    private static readonly int ParamVitesse = Animator.StringToHash("Speed");

    [Header("Santé")]
    [SerializeField] private float santeMax = 30f;

    [Header("Mort")]
    [Tooltip("Temps avant que la hyène disparaisse (laisse le temps à l'animation de mort).")]
    [SerializeField] private float delaiAvantDestruction = 1.5f;

    [Header("Retour visuel")]
    [SerializeField] private Color couleurDegats = Color.red;
    [SerializeField] private float dureeFlash = 0.1f;

    private float santeActuelle;
    private bool estMorte;

    private Rigidbody2D corps;
    private Animator animateur;
    private SpriteRenderer rendu;
    private HyeneIA intelligence;
    private Color couleurOrigine;

    private void Awake()
    {
        corps = GetComponent<Rigidbody2D>();
        animateur = GetComponentInChildren<Animator>();
        rendu = GetComponentInChildren<SpriteRenderer>();
        intelligence = GetComponent<HyeneIA>();

        if (rendu != null) couleurOrigine = rendu.color;
        santeActuelle = santeMax;
    }

    // À appeler depuis le script d'attaque du joueur
    public void PrendreDegats(float montant)
    {
        if (estMorte) return;

        santeActuelle -= montant;

        if (santeActuelle <= 0f)
        {
            Mourir();
            return;
        }

        if (rendu != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashDegats());
        }
    }

    private void Mourir()
    {
        estMorte = true;
        StopAllCoroutines();
        if (rendu != null) rendu.color = couleurOrigine;

        if (intelligence != null) intelligence.enabled = false;

        corps.linearVelocity = Vector2.zero;
        corps.simulated = false; // plus de collisions, plus de détection

        if (animateur != null)
        {
            if (ParametreExiste(ParamVitesse)) animateur.SetFloat(ParamVitesse, 0f);
            if (ParametreExiste(ParamMort)) animateur.SetTrigger(ParamMort);
        }

        Destroy(gameObject, delaiAvantDestruction);
    }

    private IEnumerator FlashDegats()
    {
        rendu.color = couleurDegats;
        yield return new WaitForSeconds(dureeFlash);
        rendu.color = couleurOrigine;
    }

    private bool ParametreExiste(int hash)
    {
        foreach (AnimatorControllerParameter p in animateur.parameters)
        {
            if (p.nameHash == hash) return true;
        }
        return false;
    }
}