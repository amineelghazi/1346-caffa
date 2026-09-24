using UnityEngine;

public class SuiviCamera : MonoBehaviour
{
    [Header("Réglages Caméra")]
    public Transform joueur;
    public Vector3 decalage = new Vector3(0f, 1f, -10f);
    public float vitesseLissage = 5f;

    [Header("Limites de la carte")]
    public bool activerLimites = true;

    [Tooltip("La caméra s'arrêtera à ce point à gauche")]
    public float limiteGaucheX = -5f;

    [Tooltip("La caméra s'arrêtera à ce point à droite")]
    public float limiteDroiteX = 15f;

    [Tooltip("Plancher de la caméra (Le bas)")]
    public float limiteBasseY = 0f;

    [Tooltip("Plafond de la caméra (Le haut)")]
    public float limiteHauteY = 10f;

    private void LateUpdate()
    {
        if (joueur == null) return;

        Vector3 positionCible = joueur.position + decalage;

        // Si les limites sont activées, on bloque la caméra
        if (activerLimites)
        {
            // Mathf.Clamp force la valeur X à rester entre la limite gauche et droite
            positionCible.x = Mathf.Clamp(positionCible.x, limiteGaucheX, limiteDroiteX);

            // Même chose pour le Y (Haut et Bas)
            positionCible.y = Mathf.Clamp(positionCible.y, limiteBasseY, limiteHauteY);
        }

        transform.position = Vector3.Lerp(transform.position, positionCible, vitesseLissage * Time.deltaTime);
    }
}