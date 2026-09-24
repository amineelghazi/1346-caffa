using UnityEngine;
using UnityEngine.UI;

public class PlayerInfection : MonoBehaviour
{
    [Header("Infection (La Peste)")]
    public float infectionMax = 100f;
    public float infectionActuelle;

    [Tooltip("Nombre de points d'infection gagnés par mètre parcouru")]
    public float infectionParMetre = 0.5f;

    [Header("Interface Visuelle")]
    [Tooltip("Glisse le Slider d'Infection ici")]
    public Slider barreDeMaladie;

    private Vector3 dernierePosition;
    private PlayerHealth santeJoueur;

    private void Start()
    {
        infectionActuelle = 0f;
        dernierePosition = transform.position;
        santeJoueur = GetComponent<PlayerHealth>();

        if (barreDeMaladie != null)
        {
            barreDeMaladie.maxValue = infectionMax;
            barreDeMaladie.value = infectionActuelle;
        }
    }

    private void Update()
    {
        float distanceParcourue = Vector3.Distance(transform.position, dernierePosition);

        if (distanceParcourue > 0.001f)
        {
            AugmenterInfection(distanceParcourue * infectionParMetre);
        }

        dernierePosition = transform.position;
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