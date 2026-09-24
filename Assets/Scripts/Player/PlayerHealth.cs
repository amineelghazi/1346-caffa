using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float vieMax = 100f;
    public float vieActuelle;

    [Tooltip("Combien de points de vie perdus par mètre parcouru ?")]
    public float degatsParMetre = 1f;

    private Vector3 dernierePosition;

    private void Start()
    {
        vieActuelle = vieMax;
        dernierePosition = transform.position;
    }

    private void Update()
    {
        // Calcule la distance parcourue depuis la dernière frame
        float distanceParcourue = Vector3.Distance(transform.position, dernierePosition);

        // Si le joueur a bougé, on le contamine progressivement
        if (distanceParcourue > 0.001f)
        {
            PrendreDegats(distanceParcourue * degatsParMetre);
        }

        // Mise à jour de la position pour la prochaine frame
        dernierePosition = transform.position;
    }

    public void PrendreDegats(float montant)
    {
        vieActuelle -= montant;
        if (vieActuelle <= 0)
        {
            vieActuelle = 0;
            Debug.Log("Le médecin a succombé à la peste...");
            // Tu peux appeler ta fonction Mourir() de PlayerMouvement ici
            GetComponent<PlayerMouvement>().Mourir();
        }
    }

    public void Soigner(float montant)
    {
        vieActuelle += montant;
        if (vieActuelle > vieMax) vieActuelle = vieMax;
        Debug.Log("Soin appliqué ! Vie actuelle : " + (int)vieActuelle);
    }
}