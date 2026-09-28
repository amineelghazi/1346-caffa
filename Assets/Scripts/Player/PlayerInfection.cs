using UnityEngine;
using UnityEngine.UI;

public class PlayerInfection : MonoBehaviour
{
    [Header("Infection (La Peste)")]
    public float infectionMax = 100f;
    public float infectionActuelle;

    // NEW : remplace infectionParMetre
    [Tooltip("Points d'infection gagnés par seconde. Temps avant la mort = Infection Max / cette valeur (ex : 100 / 0.5 = 200 secondes).")]
    public float infectionParSeconde = 0.5f;

    [Header("Interface Visuelle")]
    [Tooltip("Glisse le Slider d'Infection ici")]
    public Slider barreDeMaladie;

    private PlayerHealth santeJoueur;

    private void Start()
    {
        infectionActuelle = 0f;
        santeJoueur = GetComponent<PlayerHealth>();

        if (barreDeMaladie != null)
        {
            barreDeMaladie.maxValue = infectionMax;
            barreDeMaladie.value = infectionActuelle;
        }
    }

    private void Update()
    {
        // NEW : l'infection monte avec le temps, que le joueur bouge ou non.
        // On arrête une fois au maximum (évite de répéter le message et la mort à chaque frame).
        if (infectionActuelle >= infectionMax) return;

        AugmenterInfection(infectionParSeconde * Time.deltaTime);
    }

    public void AugmenterInfection(float montant)
    {
        infectionActuelle += montant;

        if (barreDeMaladie != null)
        {
            barreDeMaladie.value = infectionActuelle;
        }

        if (infectionActuelle >= infectionMax)
        {
            infectionActuelle = infectionMax;
            Debug.Log("L'infection a atteint 100% !");

            // Killing the player when infection reaches 100%
            if (santeJoueur != null)
            {
                santeJoueur.PrendreDegats(infectionMax);
            }
        }
    }

    public void ReduireInfection(float montant)
    {
        infectionActuelle -= montant;
        if (infectionActuelle < 0f) infectionActuelle = 0f;

        if (barreDeMaladie != null)
        {
            barreDeMaladie.value = infectionActuelle;
        }
    }
}