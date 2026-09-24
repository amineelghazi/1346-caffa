using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Santé (Points de Vie)")]
    public float santeMax = 100f;
    public float santeActuelle;

    [Header("Interface Visuelle")]
    [Tooltip("Glisse le Slider de Santé ici")]
    public Slider barreDeSante;

    private PlayerMouvement joueurMouvement;
    private bool estMort = false;

    private void Start()
    {
        santeActuelle = santeMax;

        if (barreDeSante != null)
        {
            barreDeSante.maxValue = santeMax;
            barreDeSante.value = santeActuelle;
        }

        joueurMouvement = GetComponent<PlayerMouvement>();
    }

    public void PrendreDegats(float montant)
    {
        if (estMort) return;

        santeActuelle -= montant;
        if (santeActuelle < 0f) santeActuelle = 0f;

        if (barreDeSante != null)
        {
            barreDeSante.value = santeActuelle;
        }

        if (joueurMouvement != null)
        {
            joueurMouvement.PrendreDegats();
        }

        if (santeActuelle <= 0f)
        {
            Mourir();
        }
    }

    public void Soigner(float montant)
    {
        if (estMort) return;

        santeActuelle += montant;
        if (santeActuelle > santeMax) santeActuelle = santeMax;

        if (barreDeSante != null)
        {
            barreDeSante.value = santeActuelle;
        }
    }

    private void Mourir()
    {
        if (estMort) return;
        estMort = true;
        Debug.Log("Le médecin a succombé à ses blessures...");

        if (joueurMouvement != null)
        {
            joueurMouvement.Mourir();
        }
    }
}