using UnityEngine;

public class EffetParallaxe : MonoBehaviour
{
    [Header("Réglages Parallaxe")]
    [Tooltip("0 = Le fond bouge avec la caméra (Très loin).\n1 = Le fond reste fixe par rapport au monde (Premier plan).")]
    [SerializeField] private Vector2 multiplicateurParallaxe = new Vector2(0.5f, 0f);

    private Transform cameraTransform;
    private Vector3 positionDepartCamera;
    private Vector3 positionDepartFond;

    private void Start()
    {
        // On récupère la caméra principale
        cameraTransform = Camera.main.transform;

        // On mémorise les positions de départ
        positionDepartCamera = cameraTransform.position;
        positionDepartFond = transform.position;
    }

    private void LateUpdate()
    {
        // On calcule de combien la caméra a bougé depuis le début
        Vector3 deplacementCamera = cameraTransform.position - positionDepartCamera;

        // On calcule la nouvelle position du fond en fonction du multiplicateur
        float distanceX = deplacementCamera.x * multiplicateurParallaxe.x;
        float distanceY = deplacementCamera.y * multiplicateurParallaxe.y;

        // On applique la nouvelle position
        transform.position = new Vector3(
            positionDepartFond.x + distanceX,
            positionDepartFond.y + distanceY,
            transform.position.z
        );
    }
}