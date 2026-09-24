using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    [Header("Santé")]
    public float vieMax = 100f;
    public float vieActuelle;

    [Tooltip("Combien de points de vie perdus par mètre parcouru ?")]
    public float degatsParMetre = 1f;

    [Header("Interface Visuelle")]
    [Tooltip("Glisse le Slider de la scène ici !")]
    public Slider barreDeVie; // C'est ici qu'on va lier la jauge

    private Vector3 dernierePosition;

    private void Start()
    {
        vieActuelle = vieMax;
        dernierePosition = transform.position;

        // Initialisation de la barre de vie à l'écran
        if (barreDeVie != null)
        {
            barreDeVie.maxValue = vieMax;
            barreDeVie.value = vieActuelle;
        }
    }

    private void Update()
    {
        // On calcule la distance
        float distanceParcourue = Vector3.Distance(transform.position, dernierePosition);

        // Si le joueur marche, il tombe malade
        if (distanceParcourue > 0.001f)
        {
            PrendreDegats(distanceParcourue * degatsParMetre);
        }

        dernierePosition = transform.position;
    }

    public void PrendreDegats(float montant)
    {
        vieActuelle -= montant;

        // Dès qu'on prend des dégâts, on baisse la jauge à l'écran !
        if (barreDeVie != null)
        {
            barreDeVie.value = vieActuelle;
        }

        if (vieActuelle <= 0)
        {
            vieActuelle = 0;
            Debug.Log("Le médecin a succombé à la peste...");
            GetComponent<PlayerMouvement>().Mourir();
        }
    }

    public void Soigner(float montant)
    {
        vieActuelle += montant;
        if (vieActuelle > vieMax) vieActuelle = vieMax;

        // Dès qu'on se soigne, on remonte la jauge à l'écran !
        if (barreDeVie != null)
        {
            barreDeVie.value = vieActuelle;
        }
    }
}